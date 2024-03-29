using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Teleport", fileName = "TeleportAction")]
public class TeleportAction : AIAction
{
    public float timeToTeleport = 2.0f;
    private SpriteRenderer spriteRenderer;
    private float timeAccumulator;
    private Teleporter teleporter;

    public override void Init(StateController controller)
    {
        spriteRenderer = controller.transform.GetChild(0).GetComponent<SpriteRenderer>();
        timeAccumulator = 0.0f;
        teleporter = controller.transform.GetComponentInChildren<Teleporter>();
    }


    public override void Act(StateController controller)
    {
        TryTeleport(controller);
    }

    private void TryTeleport(StateController controller)
    {
        timeAccumulator += Time.deltaTime;
        if (timeAccumulator >= timeToTeleport)
        {
            Teleport(controller);
            Color c = spriteRenderer.color;
            c.a = 0.0f;
            spriteRenderer.color = c;
        }
        else
        {
            Color c = spriteRenderer.color;
            c.a = Mathf.Lerp(1.0f, 0.0f, timeAccumulator / timeToTeleport);
            spriteRenderer.color = c;
        }
    }

    private void Teleport(StateController controller)
    {
        // pick location in teleport area and out of player FOV (if possible)
        FieldOfView fov = GameObject.FindObjectOfType<FieldOfView>();
        GameObject player = GameObject.Find("Player");
        BoxCollider2D enemyCollider = controller.GetComponentInChildren<BoxCollider2D>();

        int numFailures = -1;
        Vector3 position = new Vector3(0.0f, 0.0f, controller.transform.position.z);
        bool hit = false;
        do
        {
            if (!hit)
            {
                numFailures++;
            }
            if (numFailures > 9)
            {
                break; // don't try too hard, lets just teleport into their fov
            }
            position.x = Random.Range(teleporter.pos1.x, teleporter.pos2.x);
            position.y = Random.Range(teleporter.pos1.y, teleporter.pos2.y);
            hit = Physics.CheckBox(position, new Vector3(0.5f * enemyCollider.size.x, 0.5f * enemyCollider.size.y, 0.0f));
        } while (hit || Vector3.Distance(position, player.transform.position) < fov.getViewDistance());
        controller.transform.position = position;
        Color c = spriteRenderer.color;
        c.a = 1.0f;
        spriteRenderer.color = c;
        teleporter.finished = true;
    }
}