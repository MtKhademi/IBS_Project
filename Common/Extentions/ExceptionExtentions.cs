namespace Common.Extentions
{
    public static class ExceptionExtentions
    {
        private static List<string> GetErrosForException(Exception ex)
        {
            var oResult = new List<string>();
            if (ex != null)
            {
                oResult.Add(ex.Message);
                if (ex.InnerException != null)
                    oResult.AddRange(GetErrors(ex.InnerException));
            }

            return oResult;
        }
        public static List<string> GetErrors(this Exception ex)
        {
            return GetErrosForException(ex);
        }

    }
}
