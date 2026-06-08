namespace NewsAggregator.Domain.Entities;

public sealed class ArticleCategory
{
    private ArticleCategory()
    {
    }

    public ArticleCategory(
        Guid articleId,
        Guid categoryId)
    {
        ArticleId = articleId;
        CategoryId = categoryId;
    }

    public Guid ArticleId { get; private set; }

    public Guid CategoryId { get; private set; }

    public Article Article { get; private set; } = null!;

    public Category Category { get; private set; } = null!;
}