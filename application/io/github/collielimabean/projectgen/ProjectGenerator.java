package io.github.collielimabean.projectgen;

import javax.swing.SwingWorker;

public class ProjectGenerator extends SwingWorker<Object,Object>
{
	
	private String title;
	private int type;
	
	public ProjectGenerator(String projectTitle, int projectType)
	{
		title = projectTitle;
		type = projectType;
	}
	
	public enum PROJECT_TYPE
	{
		ECLIPSE(0), INTELLIJ(1), NETBEANS(2), OTHER(3);
		
		private final int value;
		
		PROJECT_TYPE(final int value)
		{
			this.value = value;
		}
		
		public final int getValue()
		{
			return value;
		}
				
	}
	
	@Override
	protected Object doInBackground() throws Exception 
	{
		//TODO Implement project generation
		return null;
	}
	
	@Override
	protected void done()
	{
		//TODO Update package explorer GUI
	}

}
