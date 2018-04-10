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
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, Team>>(DICTIONARY_TEAMS);
            var teams = new List<Team>();

            using (var tx = stateManager.CreateTransaction())
            {
                var enumerable = await teamsDictionary.CreateEnumerableAsync(tx);
                var enumerator = enumerable.GetAsyncEnumerator();

                while (await enumerator.MoveNextAsync(CancellationToken.None))
                {
                    teams.Add(enumerator.Current.Value);
                }
            }

            return Json(teams);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, Team>>(DICTIONARY_TEAMS);

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
        public async Task Put(string name, [FromBody]Team team)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, Team>>(DICTIONARY_TEAMS);

            using (var tx = stateManager.CreateTransaction())
            {
                await teamsDictionary.SetAsync(tx, name, team);
                await tx.CommitAsync();
            }
        }

        [HttpDelete("{name}")]
        public async Task Delete(string name)
        {
            var teamsDictionary = await stateManager.GetOrAddAsync<IReliableDictionary<string, Team>>(DICTIONARY_TEAMS);

            using (var tx = stateManager.CreateTransaction())
            {
                await teamsDictionary.TryRemoveAsync(tx, name);
                await tx.CommitAsync();
            }
        }
    }

    [DataContract]
    public class Team
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string[] Members { get; set; }

        [DataMember]
        public int Score { get; set; }

        [DataMember]
        public PowerGrid PowerGrid { get; set; }
    }

    [DataContract]
    public class PowerGrid
    {
        [DataMember]
        public int Intelligence { get; set; }

        [DataMember]
        public int Strength { get; set; }

        [DataMember]
        public int Speed { get; set; }

        [DataMember]
        public int Durability { get; set; }

        [DataMember]
        public int EnergyProjection { get; set; }

        [DataMember]
        public int FightingSkills { get; set; }
    }
}
