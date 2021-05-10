package dk.cphbusiness.mrv.twitterclone.impl;

import dk.cphbusiness.mrv.twitterclone.contract.PostManagement;
import dk.cphbusiness.mrv.twitterclone.dto.Post;
import dk.cphbusiness.mrv.twitterclone.util.Time;
import redis.clients.jedis.Jedis;
import java.util.List;
import java.util.Map;

public class PostManagementImpl implements PostManagement {
    private Jedis jedis;
    private Time time;

    public PostManagementImpl(Jedis jedis, Time time) {
        this.jedis = jedis;
        this.time = time;
    }

    @Override
    public boolean createPost(String username, String message) {
        long timestamp = time.getCurrentTimeMillis();
        if(!jedis.hexists("user#" + username, "username"))
            return false;

        jedis.hset("posts#" + username, "" + timestamp, message);
        return true;
    }

    @Override
    public List<Post> getPosts(String username) {
        Map<String, String> fields = jedis.hgetAll("posts#" + username);
        List<Post> posts = fields.keySet().stream()
            .map(s -> new Post(Long.parseLong(s), fields.get(s))).toList();
        return posts;
    }

    @Override
    public List<Post> getPostsBetween(String username, long timeFrom, long timeTo) {
        Map<String, String> fields = jedis.hgetAll("posts#" + username);
        List<Post> posts = fields.keySet().stream()
            .filter(s -> Long.parseLong(s) >= timeFrom && Long.parseLong(s) <= timeTo)
            .map(s -> new Post(Long.parseLong(s), fields.get(s))).toList();
        return posts;
    }
}
