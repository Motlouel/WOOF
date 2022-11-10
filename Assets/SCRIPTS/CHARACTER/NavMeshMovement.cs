using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
///using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Ce script contient les fonctions de déplacement des navmeshs agent
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
[AddComponentMenu("KH/NavMeshMovement")]
public class NavMeshMovement : MonoBehaviour
{

    private NavMeshAgent agent;
    //public NavMeshPath path;

    private void Awake()
    {
        if (!GetComponent<NavMeshAgent>())
        {
            agent = gameObject.AddComponent<NavMeshAgent>();
        }
        else
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    public void MoveTo(Vector3 position)
    {
        agent.isStopped = false;
        agent.destination = position;
        //NavMeshHit hit;
        //int areaMask = NavMesh.AllAreas;
        //if(NavMesh.SamplePosition(position, out hit, 5f, areaMask))
        //{
        //    NavMeshPath path = new NavMeshPath();
        //    if(NavMesh.CalculatePath(transform.position, position, areaMask, path))
        //    {
        //        this.path = path;
        //    }
        //}
        
    }

    public void StopMovement()
    {
        agent.isStopped = true;
    }

    public void Update()
    {
        //if (agent == null) return;
        //if (agent.path == null) return;
        //if (agent.path.corners.Length > 2)
        //{
        //    const float cornerAvoidance = 2f;
        //    Vector3 corner = agent.path.corners[1];
        //    NavMeshHit edgeHit;

        //    if (NavMesh.FindClosestEdge(corner, out edgeHit, NavMesh.AllAreas))
        //    {
        //        NavMeshHit avoidRayHit, toAgentRayHit;
        //        Vector3 newCorner = corner;
        //        if (NavMesh.Raycast(edgeHit.position, edgeHit.position + edgeHit.normal * cornerAvoidance * 2.0f, out avoidRayHit, NavMesh.AllAreas))
        //        {
        //            newCorner = edgeHit.position + avoidRayHit.normal * avoidRayHit.distance * 0.5f;
        //        }
        //        else
        //        {
        //            newCorner = edgeHit.position + edgeHit.normal * cornerAvoidance;
        //        }

        //        if (NavMesh.Raycast(newCorner, agent.path.corners[0], out toAgentRayHit, NavMesh.AllAreas))
        //        {
        //            // :( SAD AND UGLY!!!
        //        }
        //        else
        //        {
        //            var newPath = agent.path;
        //            newPath.corners[1] = newCorner;
        //            agent.ResetPath();
        //            agent.SetPath(newPath);
        //        }
        //    }
        //}
    }

    public void DebugDrawPath()
    {
        if (agent == null) return;
        var path = agent.path;
        if (path == null) return;
        const float cornerAvoidance = 10f;
        for (int i = 1; i < path.corners.Length; i++)
        {
            Vector3 corner = path.corners[i];
            Gizmos.DrawWireSphere(corner, 0.15f);
            NavMeshHit hit;
            if (NavMesh.FindClosestEdge(corner, out hit, NavMesh.AllAreas))
            {
                NavMeshHit rayHit, rayHit2;
                Vector3 newCorner;
                Color col = Color.green;
                if (NavMesh.Raycast(hit.position, hit.position + hit.normal * cornerAvoidance * 2.0f, out rayHit, NavMesh.AllAreas))
                {
                    newCorner = hit.position + hit.normal * rayHit.distance * 0.5f;
                    col = Color.red;
                }
                else
                {
                    newCorner = hit.position + hit.normal * cornerAvoidance;
                }
                if (NavMesh.Raycast(newCorner, path.corners[i - 1], out rayHit2, NavMesh.AllAreas))
                {

                }
                else
                {

                }
                Gizmos.color = col;
                Gizmos.DrawLine(newCorner, hit.position);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(newCorner, rayHit2.position);
                //NavMesh.mesh

            }
        }
    }

    private void OnDrawGizmos()
    {
        DebugDrawPath();
    }

}