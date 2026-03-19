using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Interfaces;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private ESpinners _name = ESpinners.Spinner;
    private ITokenChild _tokenChild;
    void Start()
    {
        _tokenChild = TokenHelper.GetTokenHolder(gameObject, _name.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        if(_tokenChild)
            transform.Rotate(new Vector3(0,0,1), 80*Time.deltaTime);
    }
}
