package io.github.collielimabean.main;

import javax.swing.SwingUtilities;
import javax.swing.UIManager;
import javax.swing.UIManager.LookAndFeelInfo;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;

public class Main 
{
	
	public static final String VERSION_NUMBER = "v1.0a";
	
	public static void main(String[] args)
	{
		SwingUtilities.invokeLater(new Runnable()
		{
			public void run()
			{
				
				setLookAndFeel();

				BukkitPluginEditorFrame frame = new BukkitPluginEditorFrame("Bukkit Plugin Editor");
				frame.setVisible(true);
				
			}
		});
	}
	
	private static void setLookAndFeel()
	{
		try
		{
			for(LookAndFeelInfo info : UIManager.getInstalledLookAndFeels())
			{
				if(info.getName().equalsIgnoreCase("nimbus"))
				{
					UIManager.setLookAndFeel(info.getClassName());
				}
			}
		}
		
		catch (Exception e)
		{
			
		}
	}
	
}
