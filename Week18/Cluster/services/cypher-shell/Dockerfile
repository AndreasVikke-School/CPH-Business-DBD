FROM openjdk:11
WORKDIR /cs

COPY . ./

RUN ["apt-get", "update"]
RUN ["apt-get", "install", "default-jre", "-y"]
RUN ["apt-get", "--fix-broken", "install"]
RUN ["dpkg", "-i", "cypher-shell_4.2.2_all.deb"]
ENTRYPOINT ["tail", "-f", "/dev/null"]