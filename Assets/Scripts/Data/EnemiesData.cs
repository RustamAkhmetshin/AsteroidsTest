using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu( fileName = "EnemiesData", menuName = "EnemiesData" )]
public class EnemiesData : ScriptableObject
{
    private const string ENUM_NAME_ENEMIES = "EnemyTypes";
    private const string ENUM_NAME_ASTEROIDS = "AsteroidTypes";
    
    public List<EnemyData> enemyTypes;
    public List<AsteroidData> asteroidTypes;
    
//    #if UNITY_EDITOR
    private void OnValidate() {
        
        
        string filePathAndNameEnemies = "Assets/Scripts/Enums/" + ENUM_NAME_ENEMIES + ".cs";
 
        using ( StreamWriter streamWriter = new StreamWriter( filePathAndNameEnemies ) )
        {
            streamWriter.WriteLine( "public enum " + ENUM_NAME_ENEMIES );
            streamWriter.WriteLine( "{" );
            for( int i = 0; i < enemyTypes.Count; i++ )
            {
                if(enemyTypes[i].IncludeInEnum)
                    streamWriter.WriteLine( "\t" + enemyTypes[i].Name + "," );
            }
            streamWriter.WriteLine( "}" );
        }
        
        string filePathAndNameAsteroids = "Assets/Scripts/Enums/" + ENUM_NAME_ASTEROIDS + ".cs";
        
        using ( StreamWriter streamWriter = new StreamWriter( filePathAndNameAsteroids ) )
        {
            
            streamWriter.WriteLine( "public enum " + ENUM_NAME_ASTEROIDS );
            streamWriter.WriteLine( "{" );
            for( int i = 0; i < asteroidTypes.Count; i++ )
            {
                if(asteroidTypes[i].IncludeInEnum)
                    streamWriter.WriteLine( "\t" + asteroidTypes[i].Name + "," );
            }
            streamWriter.WriteLine( "}" );
        }
        
     //   AssetDatabase.Refresh();
    }
 //   #endif
}
