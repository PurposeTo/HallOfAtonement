public abstract class EnemyType : CharacterType
{
    private protected EnemyPresenter EnemyPresenter;

    private void Start()
    {
        EnemyPresenter = gameObject.GetComponent<EnemyPresenter>();
    }
}
