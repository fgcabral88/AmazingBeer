namespace AmazingBeer.Api.Domain.Exceptions
{
    public class CustomExceptions 
    {
        // Exceção para recursos não encontrados
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        // Exceção para erros de validação
        public class ValidationException : Exception
        {
            public ValidationException(string message) : base(message) { }
        }

        // Exceção para erros de permissão
        public class UnauthorizedException : Exception
        {
            public UnauthorizedException(string message) : base(message) { }
        }

        // Exceção para erros de duplicação de registro
        public class DuplicateRecordException : Exception
        {
            public DuplicateRecordException(string message) : base(message) { }
        }

        // Exceção para erros genéricos de banco de dados
        public class DatabaseException : Exception
        {
            public DatabaseException(string message) : base(message) { }
        }
    }
}
