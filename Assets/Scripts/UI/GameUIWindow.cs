using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class GameUIWindow : Window
{
    [SerializeField] private Text _livesText;

    public CompositeDisposable disposables;

    void OnEnable () {
        disposables = new CompositeDisposable();
        MessageBroker.Default
            .Receive<MessageBase>() 
            .Where(msg => msg.id == ServiceShareData.MSG_ATTACK)
            .Subscribe(msg => {
                string data = (string)msg.data;

                _livesText.text = "Lives: " + data;
            }).AddTo (disposables);
    }
    
    
    void OnDisable () {
        if (disposables != null) {
            disposables.Dispose ();
        }
    }
    
    
}
