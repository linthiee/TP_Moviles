using UnityEngine;

public interface IState
{
    void Enter(MenuController menu);
    void Exit(MenuController menu);
}

public class MenuState : IState
{
    public void Enter(MenuController menu)
    {
        menu.menuPanel.SetActive(true);
    }

    public void Exit(MenuController menu)
    {
        menu.menuPanel.SetActive(false);
    }
}

public class DifficultyState : IState
{
    public void Enter(MenuController menu) 
    { 
        menu.difficultyPanel.SetActive(true); 
    }
    
    public void Exit(MenuController menu) 
    { 
        menu.difficultyPanel.SetActive(false); 
    }
}

public class CreditsState : IState
{
    public void Enter(MenuController menu) 
    { 
        menu.creditsPanel.SetActive(true); 
    }
    
    public void Exit(MenuController menu) 
    { 
        menu.creditsPanel.SetActive(false); 
    }
}