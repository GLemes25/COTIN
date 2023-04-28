namespace Common.Extensions.Logic
{
    public class Cnpj
    {
        protected Cnpj()
        {
        }

        public Cnpj(string cnpj)
        {
            try
            {
                cnpj = CnpjLimpo(cnpj);
                if (!IsCnpj(cnpj))
                    throw new Exception();

            }
            catch (Exception)
            {
                throw new Exception("Cnpj inválido: " + cnpj);
            }
        }

        public static string CnpjLimpo(string cnpj)
        {
            cnpj = Extensao.GetNumeros(cnpj);

            if (string.IsNullOrEmpty(cnpj))
                return "";
            return cnpj;
        }

        public static bool IsCnpj(string cnpj)
        {
            try
            {
                string tempCpfCnpj;
                string digito;
                int soma;
                int resto;
                int[] multiplicador1;
                int[] multiplicador2;

                cnpj = Extensao.GetNumeros(cnpj);

                multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                if (cnpj.Length != 14)
                    return false;

                if (cnpj.Substring(0, 8).Equals("00000000") || cnpj.Substring(0, 8).Equals("11111111") ||
                    cnpj.Substring(0, 8).Equals("22222222") || cnpj.Substring(0, 8).Equals("33333333") ||
                    cnpj.Substring(0, 8).Equals("44444444") || cnpj.Substring(0, 8).Equals("55555555") ||
                    cnpj.Substring(0, 8).Equals("66666666") || cnpj.Substring(0, 8).Equals("77777777") ||
                    cnpj.Substring(0, 8).Equals("88888888") || cnpj.Substring(0, 8).Equals("99999999"))
                    return false;

                tempCpfCnpj = cnpj.Substring(0, 12);
                soma = 0;

                for (int i = 0; i < 12; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador1[i];

                resto = (soma % 11);

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = resto.ToString();
                tempCpfCnpj = tempCpfCnpj + digito;

                soma = 0;
                for (int i = 0; i < 13; i++)
                    soma += int.Parse(tempCpfCnpj[i].ToString()) * multiplicador2[i];

                resto = (soma % 11);

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                digito = digito + resto.ToString();

                return cnpj.EndsWith(digito);

            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsCnpjBase(string CnpjBase)
        {
            if (CnpjBase == null)
                return false;
            else if (CnpjBase.Length != 8)
                return false;
            else
                return true;
        }
    }
}
