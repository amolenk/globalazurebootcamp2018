using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private const string DICTIONARY_TEAMS = "Teams";

        private readonly IReliableStateManager stateManager;

        public TeamsController(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, TeamState>>(DICTIONARY_TEAMS);
            var teams = new List<TeamDto>();

            using (var tx = stateManager.CreateTransaction())
            {
                var enumerable = await teamsDictionary.CreateEnumerableAsync(tx);
                var enumerator = enumerable.GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(CancellationToken.None))
                {
                    teams.Add(new TeamDto
                    {
                        Name = enumerator.Current.Key,
                        Members = enumerator.Current.Value.Members,
                        Score = enumerator.Current.Value.Score
                    });
                }
            }

            return Json(teams);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, TeamState>>(DICTIONARY_TEAMS);

            using (var tx = stateManager.CreateTransaction())
            {
                var team = await teamsDictionary.TryGetValueAsync(tx, name);

                if (team.HasValue)
                {
                    return Ok(team);
                }
            }

            return NotFound();
        }

        [HttpPut("{name}")]
        public async Task Put(string name, [FromBody]TeamDto team)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, TeamState>>(DICTIONARY_TEAMS);

            using (var tx = stateManager.CreateTransaction())
            {
                await teamsDictionary.SetAsync(tx, name, new TeamState
                {
                    Members = team.Members,
                    Score = team.Score
                });

                await tx.CommitAsync();
            }
        }

        [HttpDelete("{name}")]
        public async Task Delete(string name)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, TeamState>>(DICTIONARY_TEAMS);

            using (var tx = stateManager.CreateTransaction())
            {
                await teamsDictionary.TryRemoveAsync(tx, name);
                await tx.CommitAsync();
            }
        }
    }

    public class TeamDto
    {
        public string Name { get; set; }

        public string[] Members { get; set; }

        public int Score { get; set; }
    }

    [DataContract]
    public class TeamState
    {
        [DataMember]
        public string[] Members { get; set; }

        [DataMember]
        public int Score { get; set; }
    }
}
