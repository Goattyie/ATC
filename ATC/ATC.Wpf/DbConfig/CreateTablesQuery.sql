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