﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;
using TwitterSharp.Client;
using TwitterSharp.Response.RUser;

namespace TwitterSharp.UnitTests
{
    [TestClass]
    public class TestFollow
    {
        private async Task<bool> ContainsFollowAsync(string username, Follow follow)
        {
            if (follow.Users.Any(x => x.Username == username))
            {
                return true;
            }
            if (follow.NextAsync == null)
            {
                return false;
            }
            return await ContainsFollowAsync(username, await follow.NextAsync());
        }

        [TestMethod]
        public async Task GetUserFollowers()
        {
            var client = new TwitterClient(Environment.GetEnvironmentVariable("TWITTER_TOKEN"));
            var answer = await client.GetFollowersAsync("1433657158067896325");
            Assert.IsTrue(await ContainsFollowAsync("shirakamifubuki", answer));
        }

        [TestMethod]
        public async Task GetUserFollowing()
        {
            var client = new TwitterClient(Environment.GetEnvironmentVariable("TWITTER_TOKEN"));
            var answer = await client.GetFollowingAsync("1433657158067896325");
            Assert.IsTrue(await ContainsFollowAsync("cover_corp", answer));
        }
    }
}
