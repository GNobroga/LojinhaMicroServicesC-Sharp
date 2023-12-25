# Lojinha

É um projeto para por em prática a ideia de microserviços. Nele eu utilizo funcionalidades básicas relacionadas ao RabbitMQ (não uso exchanges como fanout, topic, etc), Duende Server como implementação do protocolo OpenID. Além disso, não utilizo um gateway pra centralizar as chamadas dos microserviços pois a ideia é realmente criar algo básico só pra deixar o conceito fixado.

## Tecnologias

#### Entity Framework

#### Auto Mapper

#### Swagger 

#### Identity Server

#### Razor

#### JwtBearer

#### Duende Server

#### RabbitMQ

## Conceito de Gateway

É basicamente um Entrypoint, assim como em redes de computadores onde um gateway é um porta de entrada e saida em microserviços a ideia é a mesma. Ao invés dos clients precisarem saber todas as urls dos microserviços a gente delega isso pra um gateway que é um microserviço que agrupa todos os outros.

