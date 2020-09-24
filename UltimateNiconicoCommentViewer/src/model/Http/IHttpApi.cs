using System.Net.Http;
using System.Threading.Tasks;

namespace UltimateNiconicoCommentViewer.src.model.Http
{
    interface IHttpApi
    {
        public Task<HttpResponseMessage> GetResponse(string param);
    }
}
