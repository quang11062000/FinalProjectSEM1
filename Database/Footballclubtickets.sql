drop database footballclubtickets;
create database if not exists footballclubtickets;
use footballclubtickets;
create table if not exists customers(
    cus_id int auto_increment primary key,
    cus_username varchar(20)  not null unique,
    cus_password varchar(20) not null,
    cus_name varchar(50),
    cus_address varchar(50),
    cus_phone varchar(20)
);
create table if not exists orders(
    order_id int auto_increment primary key,
    cus_id int,
    constraint fk_orders_customers foreign key(cus_id)
    references customers(cus_id),
	order_date datetime,
    order_status tinyint
);
create table if not exists matches(
     match_id int auto_increment primary key,
     match_name varchar(100) not null,
     match_time time not null,
     match_date date not null,
     match_stadium varchar(50)
);
create table if not exists typeoftickets(
     tickettype_name varchar(1) not null primary key,
     tickettype_price decimal(20,0)
);
create table if not exists tickets(
	ticket_id int auto_increment primary key,
    match_id int,
    ticket_type varchar(1),
    ticket_amount int,
	constraint fk_tickets_matches foreign key(match_id) references matches(match_id),
    constraint fk_tickets_typeoftickets foreign key(ticket_type) references typeoftickets(tickettype_name)
);
create table orderdetails(
     order_id int,
     ticket_id int,
     constraint fk_orderdetails_orders foreign key(order_id)
     references orders(order_id),
     constraint fk_orderdetails_tickets foreign key(ticket_id)
     references tickets(ticket_id),
	 amount int,
     unit_price decimal(20,0),
     primary key(order_id,ticket_id)
);
insert into customers(cus_username,cus_password, cus_name,cus_phone)
values("customer01","123456","Dong","037423412"),("customer02","234567","Quang","037423416");
select * from customers;
insert into matches(match_name,match_time,match_date,match_stadium)
values("Than Quang Ninh vs Hoang Anh Gia Lai","18:00","2019/7/13","SVD Cua Ong"),
("Ha Noi T&T vs Hoang Anh Gia Lai","19:00","2019/7/17","SVD Hang Day"),
("Hoang Anh Gia Lai vs Song Lam Nghe An","17:00","2019/7/21","SVD Pleiku"),
("Thanh Hoa vs Hoang Anh Gia Lai","17:00","2019/7/28","SVD Thanh Hoa"),
("Quang Nam vs Hoang Anh Gia Lai","18:00","2019/8/4","SVD Tam Ky"),
("Hoang Anh Gia Lai vs Nam Dinh","17:00","2019/7/10","SVD Pleiku");
select * from matches;
insert into typeoftickets(tickettype_name,tickettype_price)
values("A",70000),("B",60000),("C",50000),("D",40000);
insert into tickets(match_id,ticket_type,ticket_amount)
values(1,"A",2000),(1,"B",2000),(1,"C",1500),(1,"D",1500),
(2,"A",3000),(2,"B",3000),(2,"C",2000),(2,"D",2000),
(3,"A",2500),(3,"B",2500),(3,"C",1700),(3,"D",1700),
(4,"A",2000),(4,"B",2000),(4,"C",1500),(4,"D",1500),
(5,"A",2500),(5,"B",2500),(5,"C",2000),(5,"D",2000),
(6,"A",2500),(6,"B",2500),(6,"C",1700),(6,"D",1700);
select * from tickets;
select t.ticket_id,t.match_id,tkt.tickettype_name,tkt.tickettype_price,t.ticket_amount from tickets t 
inner join typeoftickets tkt on t.ticket_type = tkt.tickettype_name where t.match_id = 1;
select tkt.tickettype_price from tickets t inner join typeoftickets tkt on t.ticket_type = tkt.tickettype_name where ticket_id = 1;
select sum(amount) from customers cs inner join orders o on cs.cus_id = o.cus_id
 inner join orderdetails od on od.order_id = o.order_id 
 inner join tickets t on t.ticket_id = od.ticket_id where t.match_id = 1;



create user 'FCTUser'@'localhost' identified by '123456';
grant select,update,insert,delete on orders to 'FCTUser'@'localhost';
grant select,update,insert,delete on orderdetails to 'FCTUser'@'localhost';
grant select,update on tickets to 'FCTUser'@'localhost';
grant select,update on customers to 'FCTUser'@'localhost';
grant select on teams to  'FCTUser'@'localhost';
grant select on matches to  'FCTUser'@'localhost';
grant select on stadiums to  'FCTUser'@'localhost';
grant select on matchdetails to  'FCTUser'@'localhost';
grant lock tables on footballclubtickets.* to 'FCTUser'@'localhost';





