--
-- PostgreSQL database dump
--

-- Dumped from database version 13.1 (Debian 13.1-1.pgdg100+1)
-- Dumped by pg_dump version 13.1

-- Started on 2021-03-10 17:49:30 UTC

\connect "RelationalDatabasesExercise"


DELETE FROM public."CaretakerPet";
DELETE FROM public.pets;
DELETE FROM public.vets;
DELETE FROM public.caretakers;
DELETE FROM public.addresses;
DELETE FROM public.cities;

--
-- TOC entry 3003 (class 0 OID 17173)
-- Dependencies: 206
-- Data for Name: cities; Type: TABLE DATA; Schema: public; Owner: softdb
--
INSERT INTO public.cities VALUES (1, '2500', 'Valby');
INSERT INTO public.cities VALUES (2, '2750', 'Ballerup');


--
-- TOC entry 2999 (class 0 OID 17157)
-- Dependencies: 202
-- Data for Name: addresses; Type: TABLE DATA; Schema: public; Owner: softdb
--

INSERT INTO public.addresses VALUES (1, 'Trekronergade', '70', 1);
INSERT INTO public.addresses VALUES (2, 'Magleparken', '53', 2);


--
-- TOC entry 3001 (class 0 OID 17165)
-- Dependencies: 204
-- Data for Name: caretakers; Type: TABLE DATA; Schema: public; Owner: softdb
--

INSERT INTO public.caretakers VALUES (1, 'Asger', '12345678', 1);
INSERT INTO public.caretakers VALUES (2, 'Andreas', '7654321', 2);


--
-- TOC entry 3007 (class 0 OID 17189)
-- Dependencies: 210
-- Data for Name: vets; Type: TABLE DATA; Schema: public; Owner: softdb
--

INSERT INTO public.vets VALUES (1, '1234567', 'Asgers Dyreklinik', '12345678', 1);
INSERT INTO public.vets VALUES (2, '7654321', 'Andreas Dyreklinik', '87654321', 2);


--
-- TOC entry 3005 (class 0 OID 17181)
-- Dependencies: 208
-- Data for Name: pets; Type: TABLE DATA; Schema: public; Owner: softdb
--

INSERT INTO public.pets VALUES (1, 'Ludvig', 2, 1, 'Pet', NULL, NULL);
INSERT INTO public.pets VALUES (2, 'Ludvig 2.0', 3, 2, 'Pet', NULL, NULL);
INSERT INTO public.pets VALUES (3, 'Fido', 1, 1, 'Dog', NULL, 'F4');
INSERT INTO public.pets VALUES (4, 'Lillu', 3, 2, 'Cat', 9, NULL);


--
-- TOC entry 2997 (class 0 OID 17151)
-- Dependencies: 200
-- Data for Name: CaretakerPet; Type: TABLE DATA; Schema: public; Owner: softdb
--

INSERT INTO public."CaretakerPet" VALUES (1, 1);
INSERT INTO public."CaretakerPet" VALUES (2, 2);
INSERT INTO public."CaretakerPet" VALUES (1, 3);
INSERT INTO public."CaretakerPet" VALUES (2, 4);


--
-- TOC entry 2998 (class 0 OID 17154)
-- Dependencies: 201
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: softdb
--



--
-- TOC entry 3014 (class 0 OID 0)
-- Dependencies: 203
-- Name: addresses_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: softdb
--

SELECT pg_catalog.setval('public."addresses_Id_seq"', 2, true);


--
-- TOC entry 3015 (class 0 OID 0)
-- Dependencies: 205
-- Name: caretakers_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: softdb
--

SELECT pg_catalog.setval('public."caretakers_Id_seq"', 2, true);


--
-- TOC entry 3016 (class 0 OID 0)
-- Dependencies: 207
-- Name: cities_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: softdb
--

SELECT pg_catalog.setval('public."cities_Id_seq"', 2, true);


--
-- TOC entry 3017 (class 0 OID 0)
-- Dependencies: 209
-- Name: pets_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: softdb
--

SELECT pg_catalog.setval('public."pets_Id_seq"', 4, true);


--
-- TOC entry 3018 (class 0 OID 0)
-- Dependencies: 211
-- Name: vets_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: softdb
--

SELECT pg_catalog.setval('public."vets_Id_seq"', 2, true);


-- Completed on 2021-03-10 17:49:30 UTC

--
-- PostgreSQL database dump complete
--

