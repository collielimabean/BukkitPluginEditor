package io.github.collielimabean.internalserver.update;

import java.net.MalformedURLException;
import java.net.URL;

public class BukkitUpdater 
{
	
	public static final URL BUKKIT_UPDATE_SITE;
	
	static
	{
		
		URL temp;
		
		try
		{
			temp = new URL("http://dl.bukkit.org/api/1.0/downloads/projects/bukkit/artifacts/");
		}
		
		catch (MalformedURLException e)
		{
			temp = null;
		}
		
		BUKKIT_UPDATE_SITE = temp;
		
	}
	
	private String version;
	
	public BukkitUpdater(String versionToUpdate)
	{
		version = versionToUpdate;
	}
	
	
	
	
	
}
