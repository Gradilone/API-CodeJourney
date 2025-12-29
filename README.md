# CodeJourney – Backend API

## **Visão Geral**

Este repositório contém o **backend do aplicativo CodeJourney**, uma plataforma educacional gamificada voltada ao aprendizado de **lógica** e **linguagens de programação**.

A aplicação foi desenvolvida como uma **API REST**, responsável pela **autenticação**, **gestão de usuários** e **controle do progresso** nas jornadas de aprendizado.

---

## **Arquitetura e Decisões Técnicas**

- **Arquitetura em camadas**, separando responsabilidades entre:
  - Controllers  
  - Services  
  - Repository  
  - Models  
  - DTOs  

- Uso de **DTOs** para transporte seguro de dados e isolamento das entidades de domínio  
- **Autenticação e autorização via JWT**, garantindo acesso controlado às rotas protegidas  
- Aplicação do padrão **Repository** para desacoplar a lógica de negócio do acesso ao banco de dados  
- Estrutura orientada à **manutenibilidade**, **organização** e **expansão futura**

---

## **Objetivo do Backend**

Fornecer uma base **segura**, **organizada** e **escalável** para o aplicativo **CodeJourney**, suportando:

- **Cadastro e autenticação de usuários**
- **Gerenciamento de dados da plataforma**
- **Persistência do progresso educacional** dos usuários
