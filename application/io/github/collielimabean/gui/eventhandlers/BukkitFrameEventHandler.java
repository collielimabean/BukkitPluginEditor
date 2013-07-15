package io.github.collielimabean.gui.eventhandlers;

import io.github.collielimabean.gui.BukkitPluginEditorFrame;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public abstract class BukkitFrameEventHandler implements ActionListener 
{
	
	protected BukkitPluginEditorFrame frame;
	
	public BukkitFrameEventHandler(BukkitPluginEditorFrame frame)
	{
		this.frame = frame;
	}
	
	public abstract void actionPerformed(ActionEvent e);

}
