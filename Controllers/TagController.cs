using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MayconJr.StringParser.Models;

namespace StringParser.Controllers
{
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly TagContext _context;

        public TagController(TagContext context)
        {
            _context = context;
        }

        // POST api/values
        [HttpPost]
        public IActionResult Create([FromBody]Tag item)
        {
            SymbolStack sb = new SymbolStack();
            var response = new FTCResponse(){
                Code = FTCResponse.ERROR //Error
            };
            if(item == null)
                response.Content = FTCResponse.INVALID_REQUEST;
            else if(item.Name == null)
                response.Content = Tag.INVALID_NAME;
            else if(item.Name.Length == 0)
                response.Content = Tag.INVALID_NAME;
            else if(item.Definition == null)
                response.Content = Tag.INVALID_DEFINITION;
            else if(item.Definition.Length == 0)
                response.Content = Tag.INVALID_DEFINITION;
            else{
                bool isEscape = false;
                bool valid = true;
                foreach (char x in item.Definition)
                {
                    if(isEscape)
                    {
                        if(Tag.ESCAPE.Contains(x))
                        {
                            sb.Add(new Symbol($"\\{x}"));
                            isEscape = false;
                            continue;
                        }else
                        {
                            valid = false;
                            break;
                        }
                    }
                    if(x == Tag.ESCAPE_CHAR)
                    {
                        isEscape = true;
                        continue;
                    }
                    else
                    {
                        sb.Add(new Symbol($"{x}"));
                    }
                }
                if(valid)
                    valid = sb.Verify;
                if(!valid)
                {
                    response.Content = Tag.INVALID_DEFINITION;
                    return Ok(response);
                }
                var itens = from u in _context.Tags
                                where u.Equals(item)
                                select u;

                if(itens.Any())
                    response.Content = "Already exists a Tag with this name or definition.";

            _context.Tags.Add(item);
            _context.SaveChanges();
            response.Code = FTCResponse.INFO;
            }
            return Ok(response);
        }
    }
}
