namespace AmazingBeer.Api.Domain.Exceptions
{
    public class CustomExceptions 
    {
        // Exceção para recursos não encontrados
        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message) { }
        }

        // Exceção para erros de requisição
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message) { }
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

        // Exceção para erros de permissão
        public class ForbiddenException : Exception
        {
            public ForbiddenException(string message) : base(message) { }
        }

        // Exceção para erros de conflito
        public class ConflictException : Exception
        {
            public ConflictException(string message) : base(message) { }
        }

        // Exceção para erros de entidade não processável
        public class UnprocessableEntityException : Exception
        {
            public UnprocessableEntityException(string message) : base(message) { }
        }

        // Exceção para erros internos
        public class InternalServerErrorException : Exception
        {
            public InternalServerErrorException(string message) : base(message) { }
        }
    }
}
