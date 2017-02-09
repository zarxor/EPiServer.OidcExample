using EPiServer.Core;

namespace EPiServer.OicExample.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
