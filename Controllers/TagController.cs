using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using FTCRegex.Models;
using FTCRegex.Parser;

namespace FTCRegex.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly TagContext _context;

        public TagController(TagContext context)
        {
            _context = context;
        }

        

        // [HttpPatch("{id}")]
        // public FTCResponse Update(int id, [FromBody]FTCRequest reqItem){

        //     Tag item = new Tag(){
        //         Name = reqItem.Name,
        //         Definition = reqItem.Definition,
        //         UserId = reqItem.UserId,
        //     };
        //     Tag db = _context.Tags.FirstOrDefault(w => w.UserId == item.UserId);
        //     return new FTCResponse();
        // }
        // POST api/values
        [HttpPost]
        public FTCResponse Create([FromBody]FTCRequest reqItem)
        {
            var response = new FTCResponse(){
                Code = FTCResponse.ERROR //Error
            };
            Console.WriteLine("RECEBIDO: \n" + reqItem);
            if(reqItem == null){
                response.Content = FTCResponse.INVALID_REQUEST;
            }
            Tag item = new Tag(){
                Name = reqItem.Name,
                Definition = reqItem.Definition,
            };
            Console.WriteLine("TAG1: \n" + item);
            SymbolStack sb = new SymbolStack();
            if(item == null){
                response.Content = FTCResponse.INVALID_REQUEST;
                return response;
            }
            else if(item.Name == null)
            {
                response.Content = Tag.INVALID_NAME;
                return response;
            }
            else if(item.Name.Length == 0)
            {
                response.Content = Tag.INVALID_NAME;
                return response;
            }
            else if(item.Definition == null)
            {
                response.Content = Tag.INVALID_DEFINITION;
                return response;
            }
            else if(item.Definition.Length == 0)
            {
                response.Content = Tag.INVALID_DEFINITION;
                return response;
            }

            // var itens = from u in _context.Tags
            //                 where u.Name.Equals(item.Name) || u.Definition.Equals(item.Definition)
            //                 select u;

            // if(itens.Any())

            //Error if have repeated tags.
            if(_context.Tags.Any(u=>u.Name.Equals(item.Name) && u.UserId.Equals(reqItem.UserId)))
            // if(_context.Tags.Any(u=>u.Equals(item)))
            {
                response.Content = Tag.TAG_EXISTS;
                response.Code = FTCResponse.ERROR;
                return response;
            }
            bool isEscape = false;
            bool valid = true;
            //Processing syntax
            foreach (char x in item.Definition)
            {
                if(isEscape) //last char was '\'
                {
                    if(Tag.ESCAPE.Contains(x)) //x is a valid escape char.
                    {
                        sb.Add(new Symbol($"\\{x}")); //Add it to test semantic.
                        isEscape = false; //last char now isn't a '\'
                        continue; //next iteration.
                    }else
                    {
                        valid = false; //invalid syntax, abort.
                        break;
                    }
                }
                if(x == Tag.ESCAPE_CHAR) //x is '\', iterating searching escape-chars.
                {
                    isEscape = true;
                    continue;
                }
                else
                { //x is a single symbol, search for operators, or add it as symbol.
                    if(x == '+')
                        sb.Add(Operator.UNION);
                    else if(x == '.')
                        sb.Add(Operator.CONCAT);
                    else if(x == '*')
                        sb.Add(Operator.KLEENE);
                    else
                        sb.Add(new Symbol($"{x}"));
                }
            }

            //no syntax errors...
            if(valid)
                valid = sb.Verify;

            //Expression have syntax or semantic errors.
            if(!valid)
            {
                response.Content = Tag.INVALID_DEFINITION;
                return response;
            }

            //Invalid user.
            item.User = _context.Users.FirstOrDefault(i => i.UserId == reqItem.UserId);
            if(item.User == null){
                response.Content = String.Format(Tag.INVALID_USER, reqItem.UserId);
                response.Code = FTCResponse.ERROR;
                return response;
            }
            //Group not defined... picking default.
            if(reqItem.Group == null || reqItem.Group.Length == 0)
            {
                item.Group = _context.Groups.OrderBy(i=>i.GroupId).FirstOrDefault();
                response.Content = Group.GROUP_DEFAULT + " " + String.Format(Tag.TAG_DEFINED, item.Name);
                response.Code = FTCResponse.WARNING;
            }
            //Group doesn't exists.
            else if(!_context.Groups.Any(g=>reqItem.Group.Equals(g.Name)))
            {
                item.Group = new Group(){ Name = reqItem.Group };
                _context.Groups.Add(item.Group);
                response.Code = FTCResponse.INFO;
                response.Content = String.Format(Group.GROUP_NEW, item.Group) + String.Format(Tag.TAG_DEFINED, item.Name);
            }
            //Group Exists.
            else
            {
                item.Group = _context.Groups.FirstOrDefault(u=>u.Name.Equals(reqItem.Group));
                response.Code = FTCResponse.INFO;
                response.Content = String.Format(Tag.TAG_DEFINED, item.Name);
            }

            Console.WriteLine("TAG2: \n" + item);
            _context.Tags.Add(item);
            _context.SaveChanges();
            response.Id = item.TagId;
            return response;
        }

        // POST api/values
        [HttpPut]
        public FTCResponse Update([FromBody]FTCRequest reqItem)
        {
            Tag item = _context.Tags.FirstOrDefault(i => i.TagId == reqItem.TagId);
            bool definitionChanged = false;
            SymbolStack sb = new SymbolStack();
            var response = new FTCResponse(){
                Code = FTCResponse.ERROR //Error
            };
            //Verify if the tag exists.
            if(item == null){
                response.Content = FTCResponse.INVALID_REQUEST;
                return response;
            }
            //Changes tag name.
            if(reqItem.Name != null && reqItem.Name.Length != 0)
            {
                item.Name = reqItem.Name;
            }
            //Changes tag definition
            if(item.Definition != null && item.Definition.Length != 0)
            {
                if(!item.Definition.Equals(reqItem.Definition)){
                    item.Definition = reqItem.Definition;
                    definitionChanged = true;
                }
            }

            // var itens = from u in _context.Tags
            //                 where u.Name.Equals(item.Name) || u.Definition.Equals(item.Definition)
            //                 select u;

            // if(itens.Any())

            //Error if have repeated tags.
            if(_context.Tags.Any(u=>u.Name.Equals(item.Name) && u.TagId != reqItem.TagId))
            // if(_context.Tags.Any(u=>u.Equals(item)))
            {
                response.Content = Tag.TAG_EXISTS;
                response.Code = FTCResponse.ERROR;
                return response;
            }
            bool isEscape = false;
            bool valid = true;
            //Processing syntax
            if(definitionChanged){
                foreach (char x in item.Definition)
                {
                    if(isEscape) //last char was '\'
                    {
                        if(Tag.ESCAPE.Contains(x)) //x is a valid escape char.
                        {
                            sb.Add(new Symbol($"\\{x}")); //Add it to test semantic.
                            isEscape = false; //last char now isn't a '\'
                            continue; //next iteration.
                        }else
                        {
                            valid = false; //invalid syntax, abort.
                            break;
                        }
                    }
                    if(x == Tag.ESCAPE_CHAR) //x is '\', iterating searching escape-chars.
                    {
                        isEscape = true;
                        continue;
                    }
                    else
                    { //x is a single symbol, search for operators, or add it as symbol.
                        if(x == '+')
                            sb.Add(Operator.UNION);
                        else if(x == '.')
                            sb.Add(Operator.CONCAT);
                        else if(x == '*')
                            sb.Add(Operator.KLEENE);
                        else
                            sb.Add(new Symbol($"{x}"));
                    }
                }
            }else{
                valid = true;
            }
            //no syntax errors...
            if(valid && definitionChanged)
                valid = sb.Verify;

            //Expression have syntax or semantic errors.
            if(!valid)
            {
                response.Content = Tag.INVALID_DEFINITION;
                return response;
            }

            // //Invalid user.
            // item.User = _context.Users.FirstOrDefault(i => i.UserId == reqItem.UserId);
            // if(item.User == null){
            //     response.Content = String.Format(Tag.INVALID_TAG, reqItem.UserId);;
            //     response.Code = FTCResponse.ERROR;
            //     return response;
            // }

            // //Group not defined... picking default.
            // if(reqItem.Group == null || reqItem.Group.Length == 0)
            // {
            //     item.Group = _context.Groups.OrderBy(i=>i.GroupId).FirstOrDefault();
            //     response.Content = Group.GROUP_DEFAULT + " " + String.Format(Tag.TAG_DEFINED, item.Name);
            //     response.Code = FTCResponse.WARNING;
            // }

            //Group doesn't exists.
            if(reqItem.Group == null || reqItem.Group.Length == 0){
                //Ignore other ifs...
            }
            if(!_context.Groups.Any(g=>reqItem.Group.Equals(g.Name)))
            {
                item.Group = new Group(){ Name = reqItem.Group };
                _context.Groups.Add(item.Group);
                response.Code = FTCResponse.INFO;
                response.Content = String.Format(Group.GROUP_NEW, item.Group) + String.Format(Tag.TAG_REDEFINED, item.Name);
            }
            //Group Exists.
            else
            {
                item.Group = _context.Groups.FirstOrDefault(u=>u.Name.Equals(reqItem.Group));
                response.Code = FTCResponse.INFO;
                response.Content = String.Format(Tag.TAG_REDEFINED, item.Name);
            }
            _context.Tags.Update(item);
            // _context.Tags.Add(item);
            _context.SaveChanges();
            response.Id = item.TagId;
            return response;
        }

        [HttpGet]
        public Tag[] Get()
        {
            return _context.Tags.ToArray();
        }


        
        // GET api/tags/id
        [HttpGet("{id}")]
        public Tag Get(int id)
        {
            var itens = from u in _context.Tags
                                where u.TagId == id
                                select u;
            var ret = itens.ToArray();
            if(ret.Length > 0)
                return ret[0];
            return new Tag();
        }
    }
}
