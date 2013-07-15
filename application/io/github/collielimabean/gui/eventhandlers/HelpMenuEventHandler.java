package io.github.collielimabean.gui.eventhandlers;

import java.awt.event.ActionEvent;

import javax.swing.JMenuItem;
import javax.swing.JOptionPane;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;
import io.github.collielimabean.gui.eventhandlers.BukkitFrameEventHandler;

public class HelpMenuEventHandler extends BukkitFrameEventHandler 
{

	public HelpMenuEventHandler(BukkitPluginEditorFrame frame) 
	{
		super(frame);
	}

	@Override
	public void actionPerformed(ActionEvent event) 
	{

		JMenuItem item = (JMenuItem) event.getSource();
		
		String name = item.getText();
		
		if(name.equalsIgnoreCase("about"))
		{
			JOptionPane.showMessageDialog(frame, "Made by William Jen (2013)", 
											"About the Bukkit Plugin Editor", JOptionPane.INFORMATION_MESSAGE);
		}
		
		//TODO Add other help commands here

	}

}
