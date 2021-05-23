#!/bin/bash

set -e
run_cmd="dotnet run --no-launch-profile --urls http://*:80"

exec $run_cmd