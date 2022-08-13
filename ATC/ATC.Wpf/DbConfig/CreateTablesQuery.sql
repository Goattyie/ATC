DELETE FROM abonents; ALTER SEQUENCE abonents_id_seq RESTART WITH 1;
DELETE FROM atces; ALTER SEQUENCE atces_id_seq RESTART WITH 1;
DELETE FROM calls; ALTER SEQUENCE calls_id_seq RESTART WITH 1;
TRUNCATE areas CASCADE; ALTER SEQUENCE areas_id_seq RESTART WITH 1;
TRUNCATE benefits CASCADE; ALTER SEQUENCE benefits_id_seq RESTART WITH 1;
TRUNCATE benefit_types CASCADE; ALTER SEQUENCE benefit_types_id_seq RESTART WITH 1;
TRUNCATE cities CASCADE; ALTER SEQUENCE cities_id_seq RESTART WITH 1;
TRUNCATE countries CASCADE; ALTER SEQUENCE countries_id_seq RESTART WITH 1;
TRUNCATE social_statuses CASCADE; ALTER SEQUENCE social_statuses_id_seq RESTART WITH 1;
TRUNCATE tariffs CASCADE; ALTER SEQUENCE tariffs_id_seq RESTART WITH 1;

CREATE TABLE countries( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE cities( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    country_id INTEGER NOT NULL,
    CONSTRAINT fk_country_id FOREIGN KEY (country_id) REFERENCES countries(id)
);

CREATE TABLE areas( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    city_id INTEGER NOT NULL,
    CONSTRAINT fk_city_id FOREIGN KEY (city_id) REFERENCES cities(id)
);

CREATE TABLE tariffs( 
    id SERIAL PRIMARY KEY,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL,
    ratio FLOAT NOT NULL,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE benefit_types( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE social_statuses( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE benefits( 
    id SERIAL PRIMARY KEY,
    benefit_type_id INTEGER NOT NULL,
    conditions TEXT,
    tariff TEXT,
    photo TEXT,
    CONSTRAINT fk_benefit_type_id FOREIGN KEY (benefit_type_id) REFERENCES benefit_types(id)
);

CREATE TABLE abonents( 
    id SERIAL PRIMARY KEY,
    first_name TEXT NOT NULL,
    second_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    phone TEXT,
    benefit_id INTEGER NOT NULL,
    social_status_id INTEGER NOT NULL,
    CONSTRAINT fk_benefit_id FOREIGN KEY (benefit_id) REFERENCES benefits(id),
    CONSTRAINT fk_social_status FOREIGN KEY (social_status_id) REFERENCES social_statuses(id)
);

CREATE TABLE atces( 
    id SERIAL PRIMARY KEY,
    code TEXT NOT NULL,
    name TEXT NOT NULL,
    area_id INTEGER NOT NULL,
    address TEXT NOT NULL,
    open_year INTEGER NOT NULL,
    is_state BOOLEAN NOT NULL,
    license BOOLEAN NOT NULL,
    CONSTRAINT fk_area_id FOREIGN KEY (area_id) REFERENCES areas(id)
);

CREATE TABLE calls( 
    id SERIAL PRIMARY KEY,
    atc_id INTEGER NOT NULL,
    city_id INTEGER NOT NULL,
    phone TEXT NOT NULL,
    cost DECIMAL NOT NULL,
    call_date DATE NOT NULL,
    time TIME NOT NULL,
    tariff_id INTEGER NOT NULL,
    abonent_id INTEGER NOT NULL,
    CONSTRAINT fk_atc_id FOREIGN KEY (atc_id) REFERENCES atces(id) ON DELETE CASCADE,
    CONSTRAINT fk_city_id FOREIGN KEY (city_id) REFERENCES cities(id)  ON DELETE CASCADE,
    CONSTRAINT fk_tariff_id FOREIGN KEY (tariff_id) REFERENCES tariffs(id)  ON DELETE CASCADE,
    CONSTRAINT fk_abonent_id FOREIGN KEY (abonent_id) REFERENCES abonents(id)  ON DELETE CASCADE
);