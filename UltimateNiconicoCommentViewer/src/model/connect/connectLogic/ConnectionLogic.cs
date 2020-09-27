


using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UltimateNiconicoCommentViewer.src.common.stringList;

namespace UltimateNiconicoCommentViewer.src.model.connectLogic
{

    public class ConnectionLogic
    {

        private static HttpClient _client;

        private NetworkStream _stream;

        private TcpClient _tcp;


        public ConnectionLogic(HttpClient client)
        {
            _client = client;
        }

        public async IAsyncEnumerable<string> ConnectLive(string threadId, string host, string port)
        {
            using (_tcp = new TcpClient())
            {
                await _tcp.ConnectAsync(host, int.Parse(port));
                byte[] buffer = Encoding.UTF8.GetBytes(ApiURL.CONNECT_SEVER_URL(threadId, 0));
                using (_stream = _tcp.GetStream())
                {
                    _stream.Write(buffer, 0, buffer.Length);
                    while (_stream.CanRead)
                    {
                        using (var memory = new MemoryStream())
                        {
                            byte[] readByte = new byte[256];
                            var response = 0;
                            do
                            {
                               
                                try
                                {
                                    response = _stream.Read(readByte, 0, readByte.Length);
                                }
                                catch (Exception)
                                {
                                    break;
                                }
                                
                                if(response == 0)
                                {
                                    break;
                                }
                                memory.Write(readByte, 0, response);

                                yield return Encoding.UTF8.GetString(memory.GetBuffer(), 0, (int)memory.Length);

                            } while (_stream.DataAvailable);

                        }
                    }
                }

            }
        }

        public void Dispose()
        {
            _tcp.Client.Disconnect(false);
            
            _tcp = null;
            _stream.Close();
            _stream.Dispose();
            
            
        }




        public async Task<HttpResponseMessage> GetUserName(int userId) => await _client.GetAsync(ApiURL.GET_USER_NAME(userId));


        public async Task<bool> GetUserIcon(int userId)
        {
            var response = await _client.GetAsync(ApiURL.USER_ICON_URL(userId));
            return response.IsSuccessStatusCode;
        }


        /// <summary>
        /// 配信の状態を取得します
        /// </summary>
        /// <param name="liveId"> ライブのID (lv****) </param>
        /// <returns> XML形式で変換 </returns>
        public async Task<HttpResponseMessage> GetLiveStatus(string liveId) => await _client.GetAsync(ApiURL.LIVE_STATUS_URL(liveId));


    }
}
