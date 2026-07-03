namespace SegfyInsurance.Domain.Common.Base;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        DataCriacao = DateTime.UtcNow;
    }

    protected Entity(Guid id, DateTime dataCriacao, DateTime? dataAtualizacao = null)
    {
        Id = id;
        DataCriacao = dataCriacao;
        DataAtualizacao = dataAtualizacao;
    }

    public Guid Id { get; protected set; }
    public DateTime DataCriacao { get; protected set; }
    public DateTime? DataAtualizacao { get; protected set; }

    protected void MarcarComoAtualizado()
    {
        DataAtualizacao = DateTime.UtcNow;
    }
}
