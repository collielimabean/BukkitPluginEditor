package io.github.collielimabean.gui.eventhandlers;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;

import java.awt.event.ActionEvent;

import javax.swing.JMenuItem;

public class ConfigureMenuEventHandler extends BukkitFrameEventHandler 
{

	public ConfigureMenuEventHandler(BukkitPluginEditorFrame frame) 
	{
		super(frame);
	}

	@Override
	public void actionPerformed(ActionEvent e) 
	{
		JMenuItem item = (JMenuItem) e.getSource();
		
		String name = item.getText();
		
		if(name.equalsIgnoreCase("run configurations"))
		{
			
		}
		
		
	}

}
