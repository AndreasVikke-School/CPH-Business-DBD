#!/usr/bin/env sh

set -e

until $(curl --output /dev/null --silent --head --fail http://neo4jcore1:7474); do
    sleep 5
done
until $(curl --output /dev/null --silent --head --fail http://neo4jcore2:7475); do
    sleep 5
done
until $(curl --output /dev/null --silent --head --fail http://neo4jcore3:7476); do
    sleep 5
done

echo "==============================================================================================================\n
All Databases is up and running. The WebAPI is available at the http://localhost:8000 Use: /swagger to see API\n
=============================================================================================================="
run_cmd="dotnet run --no-launch-profile --urls http://*:80"
exec $run_cmd