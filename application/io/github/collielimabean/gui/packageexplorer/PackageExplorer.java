package io.github.collielimabean.gui.packageexplorer;

import javax.swing.JTree;

public class PackageExplorer 
{
	
	private JTree directory;
	
	public PackageExplorer()
	{
		createDirectoryView(null);
	}
	
	public JTree getJTree()
	{
		return directory;
	}
	
	public void createDirectoryView(String path)
	{
		//TODO DefaultMutableNode here - top level node
		directory = new JTree();
		
		if(path == null)
		{
			//TODO only create top node
		}
		
	}
	
	
}
