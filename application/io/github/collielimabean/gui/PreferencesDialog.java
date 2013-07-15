package io.github.collielimabean.gui;

import javax.swing.JDialog;

public class PreferencesDialog extends JDialog 
{
	
	private static String VERSION_SELECTED = "";
	
	private static final long serialVersionUID = 8418650505014509438L;
	
	public static String getVersionSelected()
	{
		return VERSION_SELECTED;
	}

}
