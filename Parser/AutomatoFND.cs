using System;
using System.Collections.Generic;

namespace TrabalhoFTC
{
    class AutomatoFND
    {
        private List<int> iniciais;
        public List<int> Iniciais
        {
            get { return this.iniciais; }
        }
        private List<int> finais;
        public List<int> Finais
        {
            get { return this.finais; }
        }
        private List<char> alfabeto;
        public List<char> Alfabeto
        {
            get { return this.alfabeto; }
        }
        private List<Dictionary<char, int[]>> matrizTransicao;
        public List<Dictionary<char, int[]>> MatrizDeTransicao
        {
            get { return this.matrizTransicao; }
        }

        public AutomatoFND(List<char> alfabeto, List<int> finais, List<int> iniciais, List<Dictionary<char, int[]>> matrizTransicao)
        {
            this.iniciais = iniciais;
            this.alfabeto = alfabeto;
            this.finais = finais;
            this.matrizTransicao = matrizTransicao;
        }

        public static AutomatoFND GetAutomatoSimboloUnitario(char simbolo)
        {
            return new AutomatoFND(new List<char>() { simbolo }, new List<int>() { 1 }, new List<int>() { 0 }, 
                new List<Dictionary<char, int[]>>()
                {
                    new Dictionary<char, int[]>()
                    {
                        { simbolo, new int[]{ 1 } }
                    },
                    new Dictionary<char, int[]>()
                    {
                        { simbolo, new int[]{  } }
                    }
                }
            );
        }

        private void UnirMatrizTransicao(AutomatoFND automato)
        {
            UnirAlfabeto(automato);
            int offset = this.GetQuantidadeDeEstados();
            foreach (var tupla in automato.MatrizDeTransicao)
            {
                Dictionary<char, int[]> auxTupla = new Dictionary<char, int[]>();
                foreach (var parChaveValor in tupla)
                {
                    int[] auxEstados = new int[parChaveValor.Value.Length];
                    for (int i = 0; i < parChaveValor.Value.Length; i++)
                    {
                        auxEstados[i] = parChaveValor.Value[i] + offset;
                    }
                    auxTupla.Add(parChaveValor.Key, auxEstados);
                }
                this.matrizTransicao.Add(auxTupla);
            }
        }

        public void ConcatenarAutomatos(AutomatoFND automato)
        {
            int offset = this.GetQuantidadeDeEstados();
            UnirMatrizTransicao(automato);

            foreach (int final in this.finais)
                AdicionarTransicao(final, (char)955, automato.Iniciais[0] + offset);

            finais.Clear();
            foreach(int final in automato.Finais){
                this.finais.Add(final + offset);
            }
        }
 
        public void UnirAutomatos(AutomatoFND automato)
        {
            int offset = this.GetQuantidadeDeEstados();
            UnirMatrizTransicao(automato);

            int novoInicial = this.GetQuantidadeDeEstados();
            this.matrizTransicao.Add(new Dictionary<char, int[]>());

            foreach(int inicial in this.iniciais)
                AdicionarTransicao(novoInicial, (char)955, inicial);
            foreach(int inicial in automato.Iniciais)
                AdicionarTransicao(novoInicial, (char)955, inicial + offset);

            this.matrizTransicao.Add(new Dictionary<char, int[]>());

            foreach (int final in this.finais)
                AdicionarTransicao(final, (char)955, novoInicial + 1);
            foreach (int final in automato.Finais)
                AdicionarTransicao(final + offset, (char)955, novoInicial + 1);

            this.iniciais.Clear();
            this.finais.Clear();
            this.iniciais.Add(novoInicial);
            this.finais.Add(novoInicial + 1);
        }

        public void AplicarFechoDeKleene()
        {
            int novoInicial = this.GetQuantidadeDeEstados();
            this.matrizTransicao.Add(new Dictionary<char, int[]>());
            this.matrizTransicao.Add(new Dictionary<char, int[]>());

            foreach (int final in this.finais)
            {
                foreach(int inicial in this.iniciais)
                {
                    AdicionarTransicao(final, (char)955, inicial);
                }
                AdicionarTransicao(final, (char)955, novoInicial+1);
            }

            foreach (int inicial in this.iniciais)
                AdicionarTransicao(novoInicial, (char)955, inicial);

            AdicionarTransicao(novoInicial, (char)955, novoInicial+1);
            this.iniciais.Clear();
            this.finais.Clear();
            this.iniciais.Add(novoInicial);
            this.finais.Add(novoInicial + 1);
        }

        public void UnirAlfabeto(AutomatoFND automato)
        {
            foreach (char c in automato.Alfabeto)
            {
                if (!this.alfabeto.Contains(c))
                    this.alfabeto.Add(c);
            }
        }

        public void AdicionarTransicao(int estado, char simbolo, int transicao)
        {
            if (matrizTransicao[estado].ContainsKey(simbolo))
            {
                List<int> estados = new List<int>();
                foreach (var inteiro in matrizTransicao[estado][simbolo])
                {
                    estados.Add(inteiro);
                }
                estados.Add(transicao);
                matrizTransicao[estado][simbolo] = estados.ToArray();
            } else
                matrizTransicao[estado].Add(simbolo, new int[] { transicao });
        }

        public Boolean TestarReconhecimentoDaPalavra(string palavra)
        {
            foreach(int inicial in iniciais)
            {
                if (PassarPalavra(palavra, 0, inicial))
                    return true;
            }
            return false;
        }

        private Boolean PassarPalavra(string palavra, int indice, int estadoAtual)
        {
            if (indice >= palavra.Length)
            {
                if (finais.Contains(estadoAtual))
                    return true;
                else if (!matrizTransicao[estadoAtual].ContainsKey((char)955))
                    return false;
            } else if (!alfabeto.Contains(palavra[indice]) || (!matrizTransicao[estadoAtual].ContainsKey(palavra[indice]) && !matrizTransicao[estadoAtual].ContainsKey((char)955)))
                return false;

            List<int> multiplosEstados = new List<int>();
            Queue<int> transicoesLambda = new Queue<int>();
            if (matrizTransicao[estadoAtual].ContainsKey((char)955))
            {
                foreach(var auxtransicoes in matrizTransicao[estadoAtual][(char)955])
                    transicoesLambda.Enqueue(auxtransicoes);
                multiplosEstados.AddRange(transicoesLambda);
            }

            if ((indice < palavra.Length) && matrizTransicao[estadoAtual].ContainsKey(palavra[indice]))
                multiplosEstados.AddRange(matrizTransicao[estadoAtual][palavra[indice]]);

            if (multiplosEstados.Count == 0)
                return false;

            bool flag = false;
            foreach (int estado in multiplosEstados)
            {
                if (transicoesLambda.Contains(estado))
                {
                    transicoesLambda.Dequeue();
                    flag = PassarPalavra(palavra, indice, estado);
                }
                else
                    flag = PassarPalavra(palavra, indice + 1, estado);
                if (flag)
                    return true;
            }
            return flag;
        }

        public int GetQuantidadeDeEstados()
        {
            return matrizTransicao.Count;
        }

        public String MatrizTransicaoToString()
        {
            String saida = "";

            for(int i=0; i<matrizTransicao.Count; i++)
            {
                saida += i + " - ";
                foreach(var par in matrizTransicao[i])
                {
                    saida += "[" + par.Key + " = ";
                    foreach(var estado in par.Value)
                    {
                        saida += estado + " ";
                    }
                    saida += "] ";
                }
                saida += "\n";
            }
            return saida;
        }

        public String FinaisToString()
        {
            String saida = "Finais: [";

            foreach(int i in finais)
            {
                saida += i + " ";
            }
            saida += "]";

            return saida;
        }

        public String IniciaisToString()
        {
            String saida = "Iniciais: [";

            foreach (int i in iniciais)
            {
                saida += i + " ";
            }
            saida += "]";

            return saida;
        }
    }
}
