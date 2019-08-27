-- Table: public.users

-- DROP TABLE public.users;

CREATE TABLE public.users
(
    id integer NOT NULL DEFAULT nextval('"Users_Id_seq"'::regclass),
    name character varying(100) COLLATE pg_catalog."default",
    age integer,
    cityid integer,
    gender integer,
    optime timestamp with time zone,
    timespan interval,
    CONSTRAINT "Users_pkey" PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.users
    OWNER to postgres;


-- Table: public.city

-- DROP TABLE public.city;

CREATE TABLE public.city
(
    id integer NOT NULL,
    name character varying(100) COLLATE pg_catalog."default",
    provinceid integer,
    CONSTRAINT city_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.city
    OWNER to postgres;


-- Table: public.province

-- DROP TABLE public.province;

CREATE TABLE public.province
(
    id integer NOT NULL,
    name character varying(100) COLLATE pg_catalog."default",
    CONSTRAINT province_pkey PRIMARY KEY (id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.province
    OWNER to postgres;


-- FUNCTION: public.myfunction(integer)

-- DROP FUNCTION public.myfunction(integer);

CREATE OR REPLACE FUNCTION public.myfunction(
	integer)
    RETURNS character varying
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
AS $BODY$

BEGIN
   RETURN 'id: ' || $1;
END;

$BODY$;

ALTER FUNCTION public.myfunction(integer)
    OWNER TO postgres;
