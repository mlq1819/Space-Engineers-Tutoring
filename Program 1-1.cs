const string Program_Name = "Program 1-1: Hello World"; //Name me!
Color DEFAULT_TEXT_COLOR=new Color(197,137,255,255); //I determine the color of the text! (RGBA)
Color DEFAULT_BACKGROUND_COLOR=new Color(44,0,88,255); //I determine the color of the screen! (RGBA)

/*
 * Assignment 1-1: Write a program that outputs "Hello World!".
*/


class Tutorial{
/*
 * This is a class.
 * You can put class global variables here that can be used by any method in the Tutorial Class.
 * Their values are saved between uses, but not between edits of the code.
 * You can also create more methods here.
*/
	
	
	
	
/*
 * This method will be called every time the programmable block is run.
 * The Main method is the "Main" entry point into the program, and should contain your logic
 * The "argument" is the words you put as the programmable block's argument
*/
	public void Tutorial_Main(string argument){
		Print("");
	}

/*
 * This method will be called every time the programmable block compiles, which happens during a recompile, and when the world starts up.
 * The Program method is your "setup" method, used to initialize objects and variables you will be using multiple times.
*/
	public void Tutorial_Program(){
		
	}

/*
 * This method will be called every time the programmable block saves, which happens during a recompile, and when the world saves.
 * The Save method is your "save" method, used to help the Program method next time that runs.
 * For example, you might want to save some data by writing data to the Storage field, which is saved when the world is closed. Then you could reference the data in Storage in Program to setup.
*/
	public void Tutorial_Save(){
		
	}
	
/*
 * This is the "Print" Method
 * It will output your information to the screen of the program
 * Use it by calling Print with a string
 * For example, calling "Print("Hello World");" will print the phrase "Hello World" to the programmable block
*/
	public static void Print(string input){
		Prog.Write(input);
	}
	
//Don't worry about anything beyond this point
	
	
	
	
	
	public string Storage{
		get{
			return Prog.P.Storage;
		}
		set{
			Prog.SetStorage(value);
		}
	}
}

//Don't worry about code down here; it all helps make this easier for you

public Program(){
	Prog.P=this;
	Prog.SetStorage=SetStorage;
	Me.CustomName=(Program_Name+" Programmable block").Trim();
	for(int i=0;i<Me.SurfaceCount;i++){
		Me.GetSurface(i).FontColor=DEFAULT_TEXT_COLOR;
		Me.GetSurface(i).BackgroundColor=DEFAULT_BACKGROUND_COLOR;
		Me.GetSurface(i).Alignment=TextAlignment.CENTER;
		Me.GetSurface(i).ContentType=ContentType.TEXT_AND_IMAGE;
	}
	Me.GetSurface(1).FontSize=2.2f;
	Me.GetSurface(1).TextPadding=40.0f;
	MyTutorial=new Tutorial();
	MyTutorial.Tutorial_Program();
	Me.GetSurface(0).WriteText("",false);
	Prog_Light=(IMyInteriorLight) GridTerminalSystem.GetBlockWithName(Prog_String+" Light");
	Prog_Light.Radius=5;
	Prog_Light.Intensity=5;
	Prog_Light.ShowInTerminal=false;
	Prog_Light.ShowInToolbarConfig=false;
	Prog_Light.Color=new Color(255,255,255,255);
	IMyTextPanel Prog_Panel=(IMyTextPanel) GridTerminalSystem.GetBlockWithName(Prog_String+" LCD");
	Prog_Panel.ContentType=ContentType.TEXT_AND_IMAGE;
	Prog_Panel.Alignment=TextAlignment.CENTER;
	Prog_Panel.ShowInTerminal=false;
	Prog_Panel.ShowInToolbarConfig=false;
}

public void Save(){
    MyTutorial.Tutorial_Save();
}

void UpdateProgramInfo(){
	cycle=(++cycle)%long.MaxValue;
	switch(loading_char){
		case '|':
			loading_char='\\';
			break;
		case '\\':
			loading_char='-';
			break;
		case '-':
			loading_char='/';
			break;
		case '/':
			loading_char='|';
			break;
	}
	Write("",false,false);
	Echo(Program_Name+" OS Cycle-"+cycle.ToString()+" ("+loading_char+")");
	Me.GetSurface(1).WriteText(Program_Name+" OS Cycle-"+cycle.ToString()+" ("+loading_char+")",false);
	seconds_since_last_update=Runtime.TimeSinceLastRun.TotalSeconds + (Runtime.LastRunTimeMs / 1000);
	Echo(ToString(FromSeconds(seconds_since_last_update))+" since last cycle");
	Time_Since_Start=UpdateTimeSpan(Time_Since_Start,seconds_since_last_update);
	Echo(ToString(Time_Since_Start)+" since last reboot\n");
	Me.GetSurface(1).WriteText("\n"+ToString(Time_Since_Start)+" since last reboot",true);
}

void Pass(){
	Prog_Light.Color=new Color(0,255,0,255);
	Prog_Light.Intensity=10;
}

void Fail(){
	Prog_Light.Color=new Color(255,0,0,255);
	Prog_Light.Intensity=5;
}

public void Main(string argument, UpdateType updateSource)
{
	UpdateProgramInfo();
	MyTutorial.Tutorial_Main(argument);
	string my_text=Me.GetSurface(0).GetText().ToLower().Trim();
	if(my_text.Length>0&&my_text[my_text.Length-1]=='!')
		my_text=my_text.Substring(0,my_text.Length-1);
	if(my_text.Equals("hello world"))
		Pass();
	else 
		Fail();
}

TimeSpan FromSeconds(double seconds){
	return (new TimeSpan(0,0,0,(int)seconds,(int)(seconds*1000)%1000));
}

TimeSpan UpdateTimeSpan(TimeSpan old,double seconds){
	return old+FromSeconds(seconds);
}

string ToString(TimeSpan ts){
	if(ts.TotalDays>=1)
		return Math.Round(ts.TotalDays,2).ToString()+" days";
	else if(ts.TotalHours>=1)
		return Math.Round(ts.TotalHours,2).ToString()+" hours";
	else if(ts.TotalMinutes>=1)
		return Math.Round(ts.TotalMinutes,2).ToString()+" minutes";
	else if(ts.TotalSeconds>=1)
		return Math.Round(ts.TotalSeconds,3).ToString()+" seconds";
	else 
		return Math.Round(ts.TotalMilliseconds,0).ToString()+" milliseconds";
}

void Write(string text,bool new_line=true,bool append=true){
	Echo(text);
	if(new_line)
		Me.GetSurface(0).WriteText(text+'\n', append);
	else
		Me.GetSurface(0).WriteText(text, append);
}

TimeSpan Time_Since_Start=new TimeSpan(0);
long cycle=0;
char loading_char='|';
double seconds_since_last_update=0;
Tutorial MyTutorial;
string Prog_String="Program 1-1";
IMyInteriorLight Prog_Light;
public bool SetStorage(string set){
	this.Storage=set;
	return true;
}

public class Prog{
	public static Func<string,bool> SetStorage;
	public static MyGridProgram P;
	public static void Write(string text,bool new_line=true,bool append=true){
		P.Echo(text);
		if(new_line)
			P.Me.GetSurface(0).WriteText(text+'\n', append);
		else
			P.Me.GetSurface(0).WriteText(text, append);
	}
}