```
watchlist
    <profile_id>
        <movie/series id>:type = <type>
        <movie/series id>:timestamp = <timestamp>
        <movie/series id>:season = <season>
        <movie/series id>:episode = <episode>

watchlist
    1234
        5:type = movie
        5:timestamp = 1600
        5:type = series
        2:timestamp = 1200
        2:season = 3
        2:episode = 2
    1235
        5:type = movie
        5:timestamp = 45000
        4:type = movie
        4:timestamp = 200
```

```
log
    <http_method>
        <path>:timestamp = <data>

log
    GET
        /api/Account/get:1621847222436 = {Id: 1}
        /api/Account/get:1621847222435 = {Id: 5}
        /api/Account/get/all:1621847222436 = {}
    POST
        /api/Account/create:1621847222478 = {email: test1@test.dk, firstname: test1, lastname: test}
        /api/Account/create:1621847222578 = {email: test2@test.dk, firstname: test2, lastname: test}
        /api/Account/create:1621847222443 = {email: test3@test.dk, firstname: test3, lastname: test}
```

```
chache:<chache_type>#<genre if exists> = <json data>

chache:Movie = {JSON DATA}
chache:MovieByGenre#Fantasy = {JSON DATA}
chache:Series = {JSON DATA}
chache:SeriesByGenre#Fantasy = {JSON DATA}
```