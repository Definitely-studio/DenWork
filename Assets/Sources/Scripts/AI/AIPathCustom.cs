using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AIPathCustom : MonoBehaviour
{

  /*
  * Позже разобью этот класс на врагов дальшго и ближнего боя
  *
  */


  /*
  * Позиция цели (игрока)
  * Дистанция до следующей точки на навигационной сетке
  * Компонент плагина A* для прокладывания пути
  * Компонент для поиска игрока
  * Текущая точка сетки
  * Информация, достиг ли AI цели или нет
  * Rigidbody2D моба
  */

  public Transform target;
  public Transform otherEnemyInfoSocket;
  public Transform otherEnemyInfo;
  public Enemy enemy;
  public PawnBase pawn;
  public float rayLenght = 1f;
  //public float movementSpeed = 10.0f;
  public float nextWaypointOfDistance = 1f;
  public float minDistanceToPlayer = 2f;


  private bool thereIsOtherAgentOnThePath = false;
  private Path path;
  private Seeker seeker;
  private int currentWaypoint = 0;
  private bool reachedEndOfPath = false;

  Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
      // подключаем необходимые компоненты
        seeker = GetComponent<Seeker>();
        enemy = GetComponent<Enemy>();
        pawn = GetComponent<PawnBase>();
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
      // запускаем поиск пути
        InvokeRepeating("UpdatePath", 0f, 0.5f);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate ()
    {

      if(enemy.state != States.dead && enemy.state != States.passive && rb){
        EnemyInfoSocketRotation();
        Movement();

      }
      else if(enemy.state == States.dead)
        CancelInvoke("UpdatePath");

    }


    void EnemyInfoSocketRotation()  // вращение колизии для отслеживания моба перд AI
    {
      if(path != null){

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position);
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;
        otherEnemyInfoSocket.eulerAngles = new Vector3(0,0, angle);
        //Debug.Log($" lookat {WeaponSokect.rotation}");
      }
    }

    void UpdatePath()
    {

      /* Ищем цель */
        seeker.StartPath(rb.position, target.position, OnPathComplete);

    }

    // метод для обновления точек
    void OnPathComplete(Path p)
    {
      if (!p.error)
      {

        path = p;
        currentWaypoint = 0;
      }
      //else
      // Debug.Log ("Path is not valid");
    }

    // движемся к цели

    void Movement()
    {
      // если пути нет, то выходим из  метода
        if (path == null)
        {
          return;
        }

        //Debug.Log("Movement");
        // проверка достигли ли мы цонца пути или нет

        if(currentWaypoint >= path.vectorPath.Count)
        {
          reachedEndOfPath = true;
        }
        else
        {
          reachedEndOfPath = false;
        }

        // если моб не достиг конца пути то двигаемся к нему
        if(!reachedEndOfPath) {

        // вычисление направления пути
        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rb.position).normalized;

      //  RaycastHit2D otherAgentInfo = Physics2D.Raycast(otherEnemyInfo.position, direction, rayLenght);

        //Debug.Log("Can we move" + enemy.otherEnemyDetector.GetCanWeMove());

        //вызов функции движения из Enemy
        // Если находисмся в состоянии поиска и нет препятствия перед АИ, то движемся
        if(Vector2.Distance(rb.position, target.position) >= minDistanceToPlayer && enemy.state == States.lookingfor && enemy.otherEnemyDetector.GetCanWeMove()){
            //Debug.Log(enemy);
            enemy.Movement(direction, enemy.maxSpeed);
          }
        }

        // проверка дистанции до следующей точки
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointOfDistance)
        {
          currentWaypoint++;
        }
    }





}
