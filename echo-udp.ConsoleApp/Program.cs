using System.Net;
using System.Text;
using System.Net.Sockets;


class Program
{
    static async Task Main()
    {

	int port = 9090;
	UdpClient udpClient = new UdpClient(port);

	//Creates an IPEndPoint to record the IP Address and port number of the sender.
	// The IPEndPoint will allow you to read datagrams sent from any source.
	IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

	while (true)
	{
	    try{
		// Blocks until a message returns on this socket from a remote host.
		var response = await udpClient.ReceiveAsync();

		await udpClient.SendAsync(response.Buffer, response.RemoteEndPoint);
		
		Console.WriteLine($"{response.RemoteEndPoint.Address.ToString()}:{response.RemoteEndPoint.Port.ToString()}");
		
		foreach (var b in response.Buffer)
		{
		    Console.Write(b.ToString("X2") + " "); // Prints each byte in hexadecimal format
		}
		Console.Write("\n");
	    }
	    catch ( Exception e ){
		Console.WriteLine(e.ToString());
	    }   
	}	    
    }
}

