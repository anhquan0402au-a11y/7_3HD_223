FROM jenkins/jenkins:latest-jdk17

USER root

RUN apt-get update \
 && apt-get install -y ca-certificates curl gnupg lsb-release \
 && install -m 0755 -d /etc/apt/keyrings \
 && curl -fsSL https://download.docker.com/linux/debian/gpg -o /etc/apt/keyrings/docker.asc \
 && chmod a+r /etc/apt/keyrings/docker.asc \
 && echo "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/debian $(. /etc/os-release && echo "$VERSION_CODENAME") stable" > /etc/apt/sources.list.d/docker.list \
 && apt-get update \
 && apt-get install -y docker-ce-cli docker-compose-plugin \
 && rm -rf /var/lib/apt/lists/*

RUN curl -fsSL https://dot.net/v1/dotnet-install.sh -o /tmp/dotnet-install.sh \
 && chmod +x /tmp/dotnet-install.sh \
 && /tmp/dotnet-install.sh --channel 8.0 --install-dir /usr/share/dotnet \
 && ln -sf /usr/share/dotnet/dotnet /usr/local/bin/dotnet \
 && rm /tmp/dotnet-install.sh

RUN curl -fsSL https://raw.githubusercontent.com/aquasecurity/trivy/main/contrib/install.sh | sh -s -- -b /usr/local/bin

COPY plugins.txt /usr/share/jenkins/ref/plugins.txt
RUN jenkins-plugin-cli --plugin-file /usr/share/jenkins/ref/plugins.txt

ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
ENV DOTNET_NOLOGO=1
ENV PATH="/usr/share/dotnet:/root/.dotnet/tools:${PATH}"