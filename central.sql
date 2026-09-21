create database if not exists central;
use central;
create table if not exists filme (
 id int auto_increment primary key,
 titulo varchar(255),
 genero varchar(255),
 ano date);