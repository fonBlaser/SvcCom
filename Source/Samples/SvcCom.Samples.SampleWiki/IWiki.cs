using SvcCom.Samples.SampleWiki.Authentication;
using SvcCom.Samples.SampleWiki.Content;

namespace SvcCom.Samples.SampleWiki;

public interface IWiki
{
    public IAuthentication Auth { get; }
    public IContent Content { get; }
}