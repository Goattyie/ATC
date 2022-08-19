CREATE TABLE countries( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL UNIQUE
);

CREATE TABLE cities( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    country_id INTEGER NOT NULL,
    CONSTRAINT fk_country_id FOREIGN KEY (country_id) REFERENCES countries(id) ON DELETE CASCADE
);

CREATE TABLE areas( 
    id SERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    city_id INTEGER NOT NULL,
    CONSTRAINT fk_city_id FOREIGN KEY (city_id) REFERENCES cities(id) ON DELETE CASCADE
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
    CONSTRAINT fk_benefit_type_id FOREIGN KEY (benefit_type_id) REFERENCES benefit_types(id) ON DELETE CASCADE
);

CREATE TABLE abonents( 
    id SERIAL PRIMARY KEY,
    first_name TEXT NOT NULL,
    second_name TEXT NOT NULL,
    last_name TEXT NOT NULL,
    phone TEXT NOT NULL,
    photo TEXT NOT NULL,
    benefit_id INTEGER NOT NULL,
    social_status_id INTEGER NOT NULL,
    CONSTRAINT fk_benefit_id FOREIGN KEY (benefit_id) REFERENCES benefits(id) ON DELETE CASCADE,
    CONSTRAINT fk_social_status FOREIGN KEY (social_status_id) REFERENCES social_statuses(id) ON DELETE CASCADE
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
    CONSTRAINT fk_area_id FOREIGN KEY (area_id) REFERENCES areas(id) ON DELETE CASCADE
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

CREATE INDEX calls_idx ON calls(id);

CREATE DOMAIN phone AS VARCHAR(13)
CHECK(
	VALUE ~ '^\d{13}$'
);

--TRIGGERS

CREATE OR REPLACE FUNCTION cities_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('cities_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER cities_before_insert_increment_trigger BEFORE INSERT ON cities
FOR EACH ROW EXECUTE PROCEDURE cities_before_insert_increment();

CREATE OR REPLACE FUNCTION countries_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('countries_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER countries_before_insert_increment_trigger BEFORE INSERT ON countries
FOR EACH ROW EXECUTE PROCEDURE countries_before_insert_increment();

CREATE OR REPLACE FUNCTION areas_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('areas_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER areas_before_insert_increment_trigger BEFORE INSERT ON areas
FOR EACH ROW EXECUTE PROCEDURE areas_before_insert_increment();

CREATE OR REPLACE FUNCTION benefit_types_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('benefit_types_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER benefit_types_insert_increment_trigger BEFORE INSERT ON benefit_types
FOR EACH ROW EXECUTE PROCEDURE benefit_types_before_insert_increment();

CREATE OR REPLACE FUNCTION social_statuses_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('social_statuses_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER social_statuses_insert_increment_trigger BEFORE INSERT ON social_statuses
FOR EACH ROW EXECUTE PROCEDURE social_statuses_before_insert_increment();

CREATE OR REPLACE FUNCTION benefits_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('benefits_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER benefits_insert_increment_trigger BEFORE INSERT ON benefits
FOR EACH ROW EXECUTE PROCEDURE benefits_before_insert_increment();

CREATE OR REPLACE FUNCTION tariffs_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('tariffs_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER tariffs_insert_increment_trigger BEFORE INSERT ON tariffs
FOR EACH ROW EXECUTE PROCEDURE tariffs_before_insert_increment();

CREATE OR REPLACE FUNCTION abonents_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('abonents_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER abonents_insert_increment_trigger BEFORE INSERT ON abonents
FOR EACH ROW EXECUTE PROCEDURE abonents_before_insert_increment();

CREATE OR REPLACE FUNCTION atces_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('atces_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER atces_insert_increment_trigger BEFORE INSERT ON atces
FOR EACH ROW EXECUTE PROCEDURE atces_before_insert_increment();

CREATE OR REPLACE FUNCTION calls_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := nextval('calls_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER calls_insert_increment_trigger BEFORE INSERT ON calls
FOR EACH ROW EXECUTE PROCEDURE calls_before_insert_increment();

CREATE OR REPLACE FUNCTION abonents_after_update() RETURNS trigger AS $$
BEGIN
	IF NEW.photo = NULL THEN
        RAISE EXCEPTION 'Путь к фото должен быть указан';
    END IF;
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER abonents_after_update_trigger AFTER UPDATE ON abonents
FOR EACH ROW EXECUTE PROCEDURE abonents_after_update();

CREATE OR REPLACE FUNCTION abonents_after_insert() RETURNS trigger AS $$
BEGIN
	IF NEW.photo = NULL THEN
        RAISE EXCEPTION 'Путь к фото должен быть указан';
    END IF;
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE FUNCTION abonents_after_delete() RETURNS trigger AS $$
BEGIN
	EXECUTE 'DELETE FROM calls WHERE phone = '' ||  OLD.phone || ''';
	RETURN NULL;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER abonents_after_delete_trigger AFTER DELETE ON abonents
FOR EACH ROW EXECUTE PROCEDURE abonents_after_delete();

CREATE OR REPLACE TRIGGER abonents_after_delete_trigger AFTER DELETE ON abonents
FOR EACH ROW EXECUTE PROCEDURE abonents_after_delete();

CREATE OR REPLACE FUNCTION cities_before_delete() RETURNS trigger AS $$
BEGIN
	IF (SELECT COUNT(*) FROM (SELECT FROM calls WHERE city_id=OLD.id) p) <> 0 THEN
		RAISE EXCEPTION 'В данном городе есть осуществленные звонки';
	END IF;
	RETURN NULL;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER cities_before_delete_trigger BEFORE DELETE ON cities
FOR EACH ROW EXECUTE PROCEDURE cities_before_delete();

CREATE OR REPLACE FUNCTION areas_before_update() RETURNS trigger AS $$
BEGIN
    IF OLD.city_id <> NEW.city_id THEN
        IF (SELECT COUNT(*) FROM (SELECT FROM areas WHERE name=NEW.name AND city_id=NEW.city_id) p) <> 0 THEN
            RAISE EXCEPTION 'Район уже существует в этом городе';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER areas_before_update_trigger BEFORE UPDATE ON areas
FOR EACH ROW EXECUTE PROCEDURE areas_before_update();