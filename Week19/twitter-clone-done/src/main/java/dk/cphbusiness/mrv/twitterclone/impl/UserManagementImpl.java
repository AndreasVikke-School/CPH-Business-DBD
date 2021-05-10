package dk.cphbusiness.mrv.twitterclone.impl;

import dk.cphbusiness.mrv.twitterclone.contract.UserManagement;
import dk.cphbusiness.mrv.twitterclone.dto.UserCreation;
import dk.cphbusiness.mrv.twitterclone.dto.UserOverview;
import dk.cphbusiness.mrv.twitterclone.dto.UserUpdate;
import redis.clients.jedis.Jedis;
import redis.clients.jedis.Pipeline;

import java.util.Map;
import java.util.Set;


public class UserManagementImpl implements UserManagement {

    private Jedis jedis;

    public UserManagementImpl(Jedis jedis) {
        this.jedis = jedis;
    }

    @Override
    public boolean createUser(UserCreation userCreation) {
        String hash = "user#" + userCreation.username;
        if(jedis.hexists(hash, "username"))
            return false;
        
        Map<String, String> user = Map.of(
            "username" , userCreation.username,
            "firstname" , userCreation.firstname,
            "lastname" , userCreation.lastname,
            "passwordHash" , userCreation.passwordHash,
            "birthday" , userCreation.birthday
        );
        
        jedis.hmset(hash, user);

        return true;
    }

    @Override
    public UserOverview getUserOverview(String username) {
        String hash = "user#" + username;
        if(!jedis.hexists(hash, "username"))
            return null;

        Map<String, String> fields = jedis.hgetAll(hash);
        UserOverview uo = new UserOverview() {
            {
                username = fields.get("username");
                firstname = fields.get("firstname");
                lastname = fields.get("lastname");
                numFollowers = jedis.smembers("followers#" + username).size();
                numFollowing = jedis.smembers("following#" + username).size();
            }
        };
        return uo;
    }

    @Override
    public boolean updateUser(UserUpdate userUpdate) {
        String hash = "user#" + userUpdate.username;
        if(!jedis.hexists(hash, "username"))
            return false;
        
        Pipeline p = jedis.pipelined();
        if (userUpdate.firstname != null)
            p.hset(hash, "firstname", userUpdate.firstname);
        if (userUpdate.lastname != null)
            p.hset(hash, "lastname", userUpdate.lastname);
        if (userUpdate.birthday != null)
            p.hset(hash, "birthday", userUpdate.birthday);
        p.sync();

        return true;
    }

    @Override
    public boolean followUser(String username, String usernameToFollow) {
        if(!jedis.hexists("user#" + username, "username") || !jedis.hexists("user#" + usernameToFollow, "username"))
            return false;

        jedis.sadd("following#" + username, usernameToFollow);
        jedis.sadd("followers#" + usernameToFollow, username);
        return true;
    }

    @Override
    public boolean unfollowUser(String username, String usernameToUnfollow) {
        if(!jedis.hexists("user#" + username, "username") || !jedis.hexists("user#" + usernameToUnfollow, "username"))
            return false;
            
        jedis.srem("following#" + username, usernameToUnfollow);
        jedis.srem("followers#" + usernameToUnfollow, username);
        return true;
    }

    @Override
    public Set<String> getFollowedUsers(String username) {
        Set<String> following = jedis.smembers("following#" + username);
        return following.size() > 0 ? following : null;
    }

    @Override
    public Set<String> getUsersFollowing(String username) {
        Set<String> followers = jedis.smembers("followers#" + username);
        return followers.size() > 0 ? followers : null;
    }

}
