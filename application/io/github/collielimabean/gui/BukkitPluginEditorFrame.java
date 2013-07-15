package io.github.collielimabean.gui;

import io.github.collielimabean.gui.eventhandlers.*;
import io.github.collielimabean.gui.packageexplorer.PackageExplorer;

import java.awt.Dimension;
import java.awt.Event;

import javax.swing.KeyStroke;

import java.awt.event.KeyEvent;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JPanel;
import javax.swing.JTabbedPane;
import javax.swing.JSplitPane;
import javax.swing.JMenuBar;
import javax.swing.JMenu;
import javax.swing.JMenuItem;

public class BukkitPluginEditorFrame extends JFrame
{
	private static final long serialVersionUID = 1L;
	private static final Dimension frameSize = new Dimension(1000, 800);
	
	private final ImageIcon BukkitEditorLogo = new ImageIcon("BukkitEditorLogo.png");
	private JMenuBar mainMenu;
	
	private ConfigureMenuEventHandler configureHandler;
	private FileMenuEventHandler fileHandler;
	private EditMenuEventHandler editHandler;
	private HelpMenuEventHandler helpHandler;
	
	public BukkitPluginEditorFrame(String title)
	{
		super(title);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setIconImage(BukkitEditorLogo.getImage());
		setSize(frameSize);

		initializeEventHandlers();
		initializeMenu();
		initializeLayout();
		initializePackageExplorer();
		initializeEditor();
		
		//add menu bar, package explorer, and editor
		setJMenuBar(mainMenu);
		
	}
	
	private JMenu initializeConfigureMenu()
	{
		JMenu configure = new JMenu("Configure");
		
		JMenuItem runConfig = new JMenuItem("Run Configurations");
		runConfig.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_F5, KeyEvent.CTRL_MASK));
		
		runConfig.addActionListener(configureHandler);
		
		configure.add(runConfig);
		
		return configure;
	}

	private void initializeEditor()
	{
	    //TODO Add tab editor here
	}

	private JMenu initializeEditMenu()
	{
		JMenu edit = new JMenu("Edit");
		JMenuItem copy = new JMenuItem("Copy");
		copy.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_C, KeyEvent.CTRL_MASK));
		
		JMenuItem cut = new JMenuItem("Cut");
		cut.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_X, KeyEvent.CTRL_MASK));
		
		JMenuItem paste = new JMenuItem("Paste");
		paste.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_V, KeyEvent.CTRL_MASK));
		
		JMenuItem preferences = new JMenuItem("Preferences");
		
		
		copy.addActionListener(editHandler);
		cut.addActionListener(editHandler);
		paste.addActionListener(editHandler);
		preferences.addActionListener(editHandler);
		
		edit.add(copy);
		edit.add(cut);
		edit.add(paste);
		edit.addSeparator();
		edit.add(preferences);
		
		return edit;
	}

	private void initializeEventHandlers() 
	{
		
		configureHandler = new ConfigureMenuEventHandler(this);
		fileHandler = new FileMenuEventHandler(this);
		editHandler = new EditMenuEventHandler(this);
		helpHandler = new HelpMenuEventHandler(this);
		
	}

	private JMenu initializeFileMenu()
	{
		JMenu file = new JMenu("File");
		
		JMenuItem newFile = new JMenuItem("New");
		newFile.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_N, Event.CTRL_MASK));
		
		JMenuItem open = new JMenuItem("Open");
		open.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_O, Event.CTRL_MASK));
		
		JMenuItem save = new JMenuItem("Save");
		save.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK));
		
		JMenuItem saveAs = new JMenuItem("Save as...");
		saveAs.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_S, Event.CTRL_MASK + Event.SHIFT_MASK));
		
		JMenuItem exportJAR = new JMenuItem("Export");
		
		JMenuItem exit = new JMenuItem("Exit");
		exit.setAccelerator(KeyStroke.getKeyStroke(KeyEvent.VK_Q, Event.CTRL_MASK));
		
		newFile.addActionListener(fileHandler);
		open.addActionListener(fileHandler);
		save.addActionListener(fileHandler);
		saveAs.addActionListener(fileHandler);
		exportJAR.addActionListener(fileHandler);
		exit.addActionListener(fileHandler);
		
		file.add(newFile);
		file.add(open);
		file.addSeparator();
		file.add(save);
		file.add(saveAs);
		file.addSeparator();
		file.add(exportJAR);
		file.addSeparator();
		file.add(exit);
		
		return file;
	}
	
	private JMenu initializeHelpMenu()
	{
		JMenu help = new JMenu("Help");
		JMenuItem updates = new JMenuItem("Updates");
		JMenuItem about = new JMenuItem("About");
		
		help.addActionListener(helpHandler);
		updates.addActionListener(helpHandler);
		about.addActionListener(helpHandler);
		
		help.add(updates);
		help.add(about);
		
		return help;
	}
	
	private void initializeMenu()
	{
		mainMenu = new JMenuBar();

		JButton run = new JButton("Run");
		run.addActionListener(new RunPluginEventHandler(this));

		mainMenu.add(initializeFileMenu());
		mainMenu.add(initializeEditMenu());
		mainMenu.add(initializeConfigureMenu());
		mainMenu.add(run);
		mainMenu.add(initializeHelpMenu());
		
	}
	
	private void initializeLayout()
	{

	}
	
	private void initializePackageExplorer()
	{
		//TODO Add Explorer here
		PackageExplorer explorer = new PackageExplorer();
	}

}
