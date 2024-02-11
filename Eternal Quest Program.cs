using System;
using System.Collections.Generic;

public abstract class Goal
{
    protected string name;
    protected int points;

    public Goal(string name)
    {
        this.name = name;
        points = 0;
    }
 public abstract void RecordEvent();
 public virtual void DisplayStatus()
    {
        Console.WriteLine($"{name}: [ ]");
    }
    public int GetPoints()
    {
        return points;
    }
}
public class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points) : base(name)
    {
        this.points = points;
    }
    public override void RecordEvent()
    {
        Console.WriteLine($"Completed {name}! +{points} points");
    }
    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: [X]");
    }
}
public class EternalGoal : Goal
{
    public EternalGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {name}. +{points} points");
    }
}
public class ChecklistGoal : Goal
{
    private int targetCount;
    private int completedCount;

    public ChecklistGoal(string name, int points, int targetCount) : base(name)
    {
        this.points = points;
        this.targetCount = targetCount;
        completedCount = 0;
    }
    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded {name}. +{points} points");

        completedCount++;
        if (completedCount == targetCount)
        {
            Console.WriteLine($"Bonus: {name} completed {targetCount} times! +{points * 2} bonus points");
            points += points * 2; // Bonus points
            completedCount = 0; // Reset completed count
        }
    }

    public override void DisplayStatus()
    {
        Console.WriteLine($"{name}: Completed {completedCount}/{targetCount} times");
    }
}
// User class to manage goals and score
public class User
{
    private List<Goal> goals;
    private int score;

    public User()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex >= 0 && goalIndex < goals.Count)
        {
            goals[goalIndex].RecordEvent();
            score += goals[goalIndex].GetPoints();
        }
    }

    public void DisplayGoals()
    {
        Console.WriteLine("Current Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.Write($"{i + 1}. ");
            goals[i].DisplayStatus();
        }

        Console.WriteLine($"\nTotal Score: {score} points");
    }
}

class Program
{
    static void Main()
    {
        User user = new User();

        // Adding sample goals
        user.AddGoal(new SimpleGoal("Run a Marathon", 1000));
        user.AddGoal(new EternalGoal("Read Scriptures", 100));
        user.AddGoal(new ChecklistGoal("Attend Temple", 50, 10));

        // Recording events
        user.RecordEvent(0); // Run a Marathon
        user.RecordEvent(1); // Read Scriptures
        user.RecordEvent(2); // Attend Temple (1st time)
        user.RecordEvent(2); // Attend Temple (2nd time)

        // Displaying goals and score
        user.DisplayGoals();
    }
}
