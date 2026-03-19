using STMS.Tokens.TokenEntities.Implementation;
using UnityEngine;
using UnityEngine.InputSystem;
using static Inputs;

public class SpinnerManager : MonoBehaviour,  IToggleBasicSpinnerActions
{

    private Inputs inputs;

    public void OnNewaction(InputAction.CallbackContext context)
    {
        TokenHelper.ToggleTokenValue(gameObject, ESpinners.Spinner.ToString());
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnEnable()
    {
        inputs = new Inputs();
        inputs.ToggleBasicSpinner.SetCallbacks(this);
    }
    void Start()
    {
        if (TokenManager.tokenManager == null)
            new TokenManager();
    }


}
