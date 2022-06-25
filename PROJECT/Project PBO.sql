--
-- NOTE:
--
-- File paths need to be edited. Search for $$PATH$$ and
-- replace it with the path to the directory containing
-- the extracted data files.
--
--
-- PostgreSQL database dump
--

-- Dumped from database version 14.2
-- Dumped by pg_dump version 14.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE "Project PBO";
--
-- Name: Project PBO; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "Project PBO" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Indonesian_Indonesia.1252';


ALTER DATABASE "Project PBO" OWNER TO postgres;

\connect -reuse-previous=on "dbname='Project PBO'"

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: barang; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.barang (
    kode_barang character varying(10) NOT NULL,
    nama_barang character varying(200) NOT NULL,
    harga_beli integer,
    harga_jual integer,
    jumlah_barang integer,
    satuan_barang character varying(50)
);


ALTER TABLE public.barang OWNER TO postgres;

--
-- Name: detail_transaksi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.detail_transaksi (
    no_kwitansi character varying(20) NOT NULL,
    kode_barang character varying(10) NOT NULL,
    jumlah_barang integer NOT NULL,
    harga_barang integer
);


ALTER TABLE public.detail_transaksi OWNER TO postgres;

--
-- Name: kasir; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.kasir (
    kode_kasir character varying(6) NOT NULL,
    nama_kasir character varying(150) NOT NULL,
    password_kasir character varying(50) NOT NULL,
    level_kasir character varying(50)
);


ALTER TABLE public.kasir OWNER TO postgres;

--
-- Name: transaksi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.transaksi (
    no_kwitansi character varying(20) NOT NULL,
    tgl_kwitansi date NOT NULL,
    kode_kasir character varying(6) NOT NULL
);


ALTER TABLE public.transaksi OWNER TO postgres;

--
-- Data for Name: barang; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.barang (kode_barang, nama_barang, harga_beli, harga_jual, jumlah_barang, satuan_barang) FROM stdin;
\.
COPY public.barang (kode_barang, nama_barang, harga_beli, harga_jual, jumlah_barang, satuan_barang) FROM '$$PATH$$/3321.dat';

--
-- Data for Name: detail_transaksi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.detail_transaksi (no_kwitansi, kode_barang, jumlah_barang, harga_barang) FROM stdin;
\.
COPY public.detail_transaksi (no_kwitansi, kode_barang, jumlah_barang, harga_barang) FROM '$$PATH$$/3323.dat';

--
-- Data for Name: kasir; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.kasir (kode_kasir, nama_kasir, password_kasir, level_kasir) FROM stdin;
\.
COPY public.kasir (kode_kasir, nama_kasir, password_kasir, level_kasir) FROM '$$PATH$$/3320.dat';

--
-- Data for Name: transaksi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.transaksi (no_kwitansi, tgl_kwitansi, kode_kasir) FROM stdin;
\.
COPY public.transaksi (no_kwitansi, tgl_kwitansi, kode_kasir) FROM '$$PATH$$/3322.dat';

--
-- Name: barang pk_kode_barang; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.barang
    ADD CONSTRAINT pk_kode_barang PRIMARY KEY (kode_barang);


--
-- Name: kasir pk_kode_kasir; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.kasir
    ADD CONSTRAINT pk_kode_kasir PRIMARY KEY (kode_kasir);


--
-- Name: transaksi transaksi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.transaksi
    ADD CONSTRAINT transaksi_pkey PRIMARY KEY (no_kwitansi);


--
-- PostgreSQL database dump complete
--

