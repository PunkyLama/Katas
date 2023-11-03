namespace Domain.Mappers
{
    public interface IMapper<I, O>
    {
        public O MapFrom(I input);
        public I MapTo(O output);

    }
}
