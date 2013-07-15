package io.github.collielimabean.internalserver;

import java.io.File;

/**
 * This class is where the CraftBukkit server is invoked.
 * @author William Jen
 *
 */
public class Server
{
	
	private File craftBukkit;
	
	/**
	 * Constructs a server instance given the CraftBukkit jar file path.
	 * @param The path of the CraftBukkit version used.
	 */
	public Server(String craftBukkitPath)
	{
		craftBukkit = new File(craftBukkitPath);
	}
	
	public void run()
	{
		if(craftBukkit.getAbsolutePath().endsWith(".jar"))
		{
			ProcessBuilder builder = new ProcessBuilder();
		}
		
		else
		{
			throw new IllegalArgumentException("File is of incorrect type (must be .jar)");
		}
	}
	
}
