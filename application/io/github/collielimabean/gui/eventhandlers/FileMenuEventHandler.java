package io.github.collielimabean.gui.eventhandlers;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;
import io.github.collielimabean.projectgen.*;
import java.awt.event.ActionEvent;

import javax.swing.JMenuItem;

public class FileMenuEventHandler extends BukkitFrameEventHandler
{

	public FileMenuEventHandler(BukkitPluginEditorFrame frame) 
	{
		super(frame);
	}

	@Override
	public void actionPerformed(ActionEvent event)
	{
		
		JMenuItem item = (JMenuItem) event.getSource();
		
		String itemName = item.getText();
		
		if(itemName.equalsIgnoreCase("new"))
		{
			//TODO new file wizard
		}
		
		else if(itemName.equalsIgnoreCase("exit"))
		{
			frame.dispose();
		}
		

	}


}
