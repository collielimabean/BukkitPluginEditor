package io.github.collielimabean.gui.eventhandlers;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;

import java.awt.event.ActionEvent;

import javax.swing.JMenuItem;
import javax.swing.SwingWorker;

public class EditMenuEventHandler extends BukkitFrameEventHandler
{
	
	public EditMenuEventHandler(BukkitPluginEditorFrame frame)
	{
		super(frame);
	}
	
	@Override
	public void actionPerformed(ActionEvent event)
	{
		//TODO Implement actions for items
		
		JMenuItem item = (JMenuItem) event.getSource();
		
		String itemText = item.getText();
		
		if(itemText.equalsIgnoreCase("copy"))
		{
			//TODO Once editor implemented, getEditor() and copy()/paste()/cut() respectively
		}
		
		else if(itemText.equalsIgnoreCase("cut"))
		{
			//see above
			

		}
		
		else if(itemText.equalsIgnoreCase("paste"))
		{
			//see above
		}
		
	}
	
}
