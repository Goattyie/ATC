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
	NEW.id := currval('cities_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER cities_before_insert_increment_trigger BEFORE INSERT ON cities
FOR EACH ROW EXECUTE PROCEDURE cities_before_insert_increment();

CREATE OR REPLACE FUNCTION countries_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('countries_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER countries_before_insert_increment_trigger BEFORE INSERT ON countries
FOR EACH ROW EXECUTE PROCEDURE countries_before_insert_increment();

CREATE OR REPLACE FUNCTION areas_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('areas_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER areas_before_insert_increment_trigger BEFORE INSERT ON areas
FOR EACH ROW EXECUTE PROCEDURE areas_before_insert_increment();

CREATE OR REPLACE FUNCTION benefit_types_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('benefit_types_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER benefit_types_insert_increment_trigger BEFORE INSERT ON benefit_types
FOR EACH ROW EXECUTE PROCEDURE benefit_types_before_insert_increment();

CREATE OR REPLACE FUNCTION social_statuses_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('social_statuses_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER social_statuses_insert_increment_trigger BEFORE INSERT ON social_statuses
FOR EACH ROW EXECUTE PROCEDURE social_statuses_before_insert_increment();

CREATE OR REPLACE FUNCTION benefits_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('benefits_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER benefits_insert_increment_trigger BEFORE INSERT ON benefits
FOR EACH ROW EXECUTE PROCEDURE benefits_before_insert_increment();

CREATE OR REPLACE FUNCTION tariffs_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('tariffs_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER tariffs_insert_increment_trigger BEFORE INSERT ON tariffs
FOR EACH ROW EXECUTE PROCEDURE tariffs_before_insert_increment();

CREATE OR REPLACE FUNCTION abonents_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('abonents_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER abonents_insert_increment_trigger BEFORE INSERT ON abonents
FOR EACH ROW EXECUTE PROCEDURE abonents_before_insert_increment();

CREATE OR REPLACE FUNCTION atces_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('atces_id_seq');
	RETURN NEW;
END;
$$ LANGUAGE plpgsql SECURITY DEFINER;

CREATE OR REPLACE TRIGGER atces_insert_increment_trigger BEFORE INSERT ON atces
FOR EACH ROW EXECUTE PROCEDURE atces_before_insert_increment();

CREATE OR REPLACE FUNCTION calls_before_insert_increment() RETURNS trigger AS $$
BEGIN
	NEW.id := currval('calls_id_seq');
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

--ЗАПРОСЫ

--Вывести телефоны абонентов с указаным соц. статусом
CREATE OR REPLACE FUNCTION get_abonents_by_social_status(social_status TEXT)
RETURNS TABLE(first_name TEXT, second_name TEXT, phone TEXT)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.first_name, a.second_name, a.phone
		FROM abonents a
		INNER JOIN social_statuses s ON s.id = a.social_status_id
		WHERE s.name = social_status;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_abonents_by_social_status('Учитель')

--Вывести все районы указанного города 
CREATE OR REPLACE FUNCTION get_areas_by_city(city TEXT)
RETURNS TABLE(name TEXT)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.name 
		FROM areas a
		INNER JOIN cities c ON c.id = a.city_id
		WHERE c.name = city;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_areas_by_city('Таганрог')

--Вывести все звонки, совершенные после указанной даты
CREATE OR REPLACE FUNCTION get_calls_by_call_date(call_dt DATE)
RETURNS TABLE("from" TEXT, "to" TEXT, call_date DATE)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.phone, c.phone, c.call_date 
		FROM calls c
        INNER JOIN abonents a ON a.id = c.abonent_id
		WHERE c.call_date > call_dt;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_calls_by_call_date('2022-06-20')

--Вывести тарифы которые заканчиваются в указанную дату
CREATE OR REPLACE FUNCTION get_tariffs_by_end_date(end_dt DATE)
RETURNS TABLE(name TEXT)
AS $$
BEGIN
	RETURN QUERY
		SELECT tariffs.name 
		FROM tariffs
		WHERE end_date = end_dt;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_tariffs_by_end_date('2022-11-22')

--Вывести абонентов и их соц. положения
CREATE OR REPLACE VIEW get_abonents_info AS
SELECT a.second_name, a.first_name, a.last_name, a.phone, s.name AS social_status_name
FROM abonents a
INNER JOIN social_statuses s ON s.id = a.social_status_id;

SELECT * FROM get_abonents_info

--Вывести абонентов и их льготный тариф
CREATE OR REPLACE VIEW get_abonents_benefit_info AS
SELECT a.second_name, a.first_name, a.last_name, a.phone, b.tariff AS tariff
FROM abonents a
INNER JOIN benefits b ON b.id = a.benefit_id;

SELECT * FROM get_abonents_benefit_info

--Вывести АТС и их районы
CREATE OR REPLACE VIEW get_atces_area_info AS
SELECT atc.name, atc.address, a.name AS area_name
FROM atces atc
INNER JOIN areas a ON a.id = atc.area_id;

SELECT * FROM get_atces_area_info

--Вывести звонки, коэффициент тарифа которых не равен 1
CREATE OR REPLACE VIEW get_calls_where_tariff_ratio_is_not_null AS
SELECT a.phone AS "from", c.phone AS "to", c.call_date, t.ratio
FROM calls c
INNER JOIN abonents a ON a.id = c.abonent_id
LEFT OUTER JOIN tariffs t ON t.id = c.tariff_id
WHERE t.ratio != 1;

SELECT * FROM get_calls_where_tariff_ratio_is_not_null

--Вывести информацию о абонентах, которые хоть раз совершали звонки
CREATE OR REPLACE VIEW get_abonents_have_calls AS
SELECT a.first_name, a.second_name, a.last_name, a.phone 
FROM calls c
RIGHT OUTER JOIN abonents a ON a.id = c.abonent_id
WHERE a.id IS NOT NULL;

SELECT * FROM get_abonents_have_calls

-- Вывести информацию о абонентах, звонивших на указанный номер
CREATE OR REPLACE FUNCTION get_abonents_have_calls_by_phone(ph TEXT)
RETURNS TABLE(first_name TEXT, second_name TEXT, last_name TEXT, phone TEXT, call_date DATE)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.first_name, a.second_name, a.last_name, a.phone, c.call_date
		FROM abonents a
		LEFT JOIN calls c ON a.id = c.abonent_id
		WHERE c.phone = ph;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_abonents_have_calls_by_phone('+77423780946')

-- Вывести общее время разговоров на всех АТС
CREATE OR REPLACE VIEW get_time_calls_sum AS
SELECT a.name, a.address, SUM(c.time)
FROM calls c
JOIN atces a ON c.atc_id = a.id
GROUP BY a.name, a.address;

SELECT * FROM get_time_calls_sum

-- Вывести количество абонентов с указанным соц. положением
CREATE OR REPLACE FUNCTION get_count_abonents_by_social_status(social_status_name TEXT)
RETURNS TABLE("count" BIGINT)
AS $$
BEGIN
	RETURN QUERY
		SELECT COUNT(a.phone)
		FROM abonents a
		JOIN social_statuses s ON a.social_status_id = s.id
		WHERE s.name = social_status_name
		GROUP BY s.name;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_count_abonents_by_social_status('Учитель')

-- Вывести абонентов, которые поговорили больше указанного времени
CREATE OR REPLACE FUNCTION get_abonents_by_call_time_sum(sum_time INTERVAL)
RETURNS TABLE(first_name TEXT, second_name TEXT, last_name TEXT, phone TEXT, s_time INTERVAL)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.first_name, a.second_name, a.last_name, a.phone, SUM(c.time)
		FROM calls c
		JOIN abonents a ON a.id = c.abonent_id
		GROUP BY a.first_name, a.second_name, a.last_name, a.phone
		HAVING SUM(c.time) > sum_time;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_abonents_by_call_time_sum('02:00:00')

-- Вывести социальные положения и их количество абонентов, в которых встречается введенная подстрока 
CREATE OR REPLACE FUNCTION get_social_status_by_mask(val TEXT)
RETURNS TABLE(name TEXT, "count" BIGINT)
AS $$
BEGIN
	RETURN QUERY
		SELECT s.name, COUNT(a.phone)
		FROM abonents a
		JOIN social_statuses s ON s.id = a.social_status_id
		WHERE s.name LIKE '%' || val || '%'
		GROUP BY s.name;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_social_status_by_mask('Учи');

-- Вывести абонентов, которые потратили на звонки сумму более указанного числа
CREATE OR REPLACE FUNCTION get_abonents_by_calls_cost_sum(sum_value DOUBLE PRECISION)
RETURNS TABLE(first_name TEXT, second_name TEXT, last_name TEXT, phone TEXT, "Сумма" NUMERIC)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.first_name, a.second_name, a.last_name, a.phone, SUM(c.cost)
		FROM abonents a
		JOIN calls c ON a.id = c.abonent_id
		GROUP BY a.first_name, a.second_name, a.last_name, a.phone
		HAVING SUM(c.cost) > sum_value;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_abonents_by_calls_cost_sum(100);

-- Вывести абонентов, которые потратили на звонки сумму более указанного числа в указанную дату
CREATE OR REPLACE FUNCTION get_abonents_by_calls_cost_sum(sum_value DOUBLE PRECISION, c_date DATE)
RETURNS TABLE(first_name TEXT, second_name TEXT, last_name TEXT, phone TEXT, "Сумма" NUMERIC)
AS $$
BEGIN
	RETURN QUERY
		SELECT a.first_name, a.second_name, a.last_name, a.phone, SUM(c.cost)
		FROM abonents a
		JOIN calls c ON a.id = c.abonent_id
        WHERE c.call_date = c_date
		GROUP BY a.first_name, a.second_name, a.last_name, a.phone
		HAVING SUM(c.cost) > sum_value;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_abonents_by_calls_cost_sum(0, '2000-04-16');

-- Вывести станции и их общую прибыль за звонки до и после инфляции (уменьшение на 30%), в период за между двумя датами
CREATE OR REPLACE FUNCTION get_firms_sum_connection_cost_inflation(first_date DATE, second_date DATE)
RETURNS TABLE(name TEXT, "before" NUMERIC, "after" NUMERIC)
AS $$
BEGIN
	RETURN QUERY
	SELECT a.name, SUM(c.cost), 0.7 * SUM(c.cost) :: NUMERIC
	FROM (SELECT c.cost, c.atc_id FROM calls c 
		 WHERE c.call_date BETWEEN first_date AND second_date) c
	LEFT JOIN atces a ON a.id = c.atc_id
	GROUP BY a.name;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_firms_sum_connection_cost_inflation('2022-05-20', '2022-06-20');

-- Вывести все районы, города и страны в один список
CREATE OR REPLACE VIEW get_areas_with_cities_and_counties AS
SELECT id, name FROM countries
UNION SELECT id, name FROM cities
UNION SELECT id, name FROM areas;

SELECT * FROM get_areas_with_cities_and_counties;

--Вывести все звонки, станции которых находятся в районе с указанным именем
CREATE OR REPLACE FUNCTION get_calls_by_atc_area_name(area_name TEXT)
RETURNS TABLE("from" TEXT, "to" TEXT, "call_date" DATE)
AS $$
BEGIN
	RETURN QUERY
	SELECT a.phone, c.phone, c.call_date
    FROM calls c
    JOIN abonents a ON a.id = c.abonent_id
    WHERE c.atc_id IN
    (SELECT a.id FROM atces a JOIN areas ar ON ar.id = a.area_id WHERE ar.name = area_name);
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_calls_by_atc_area_name('Ленинский');

--Вывести все звонки, станции которых не находятся в районе с указанным именем
CREATE OR REPLACE FUNCTION get_calls_no_by_atc_area_name(area_name TEXT)
RETURNS TABLE("from" TEXT, "to" TEXT, "call_date" DATE)
AS $$
BEGIN
	RETURN QUERY
	SELECT a.phone, c.phone, c.call_date
    FROM calls c
    JOIN abonents a ON a.id = c.abonent_id
    WHERE c.atc_id NOT IN
    (SELECT a.id FROM atces a JOIN areas ar ON ar.id = a.area_id WHERE ar.name = area_name);
END;
$$ LANGUAGE plpgsql;

SELECT * FROM get_calls_no_by_atc_area_name('Ленинский');

--Вывести среднюю стоимость звонка указанного абонента
CREATE OR REPLACE FUNCTION case_query(abonent_phone TEXT)
RETURNS TABLE("cost" FLOAT8)
AS $$
BEGIN
	RETURN QUERY
        SELECT AVG(CASE WHEN a.phone = abonent_phone THEN c.cost END):: FLOAT8
        FROM calls c
        JOIN abonents a ON a.id = c.abonent_id
        WHERE a.phone = abonent_phone
        GROUP BY a.phone;
END;
$$ LANGUAGE plpgsql;

SELECT * FROM case_query('+77685917265');

--Вывести разницу между общей стоимостью и средней стоимостью звонков
CREATE OR REPLACE VIEW get_diff_call_cost AS
    SELECT a.first_name, a.second_name, a.last_name, a.phone, sum(c.cost) - avg(c.cost)
    FROM calls c
    JOIN abonents a ON a.id = c.abonent_id
    GROUP BY a.first_name, a.second_name, a.last_name, a.phone, c.cost;

SELECT * FROM get_diff_call_cost;

--Кто чаще из соц. положений пользуется услугами АТС
CREATE OR REPLACE VIEW get_atces_popular_statuses AS
	SELECT a.name AS "atc", (SELECT s.name
                	FROM calls c
                	JOIN atces atc ON atc.id = c.atc_id
                	JOIN abonents ab ON ab.id = c.abonent_id
                	JOIN social_statuses s ON s.id = ab.social_status_id
                	WHERE atc.name = a.name
                	GROUP BY a.name, s.name
                	ORDER BY COUNT(s.name) DESC LIMIT 1)
	FROM calls c
	JOIN atces a ON a.id = c.atc_id
	JOIN abonents ab ON ab.id = c.abonent_id
	JOIN social_statuses s ON s.id = ab.social_status_id
	GROUP BY a.name;

SELECT * FROM get_atces_popular_statuses;

--Кто чаще из соц. положений пользуется услугами АТС (города)
CREATE OR REPLACE VIEW get_cities_popular_statuses AS
    SELECT city.name AS "city", (SELECT s.name
                    FROM calls c
                    JOIN cities ct ON ct.id = c.city_id
                    JOIN abonents ab ON ab.id = c.abonent_id
                    JOIN social_statuses s ON s.id = ab.social_status_id
                    WHERE ct.name = city.name
                    GROUP BY ct.name, s.name
                    ORDER BY COUNT(s.name) DESC LIMIT 1)
    FROM calls c
    JOIN cities city ON city.id = c.city_id
    JOIN abonents ab ON ab.id = c.abonent_id
    JOIN social_statuses s ON s.id = ab.social_status_id
    GROUP BY city.name;
    
SELECT * FROM get_cities_popular_statuses;

--Определить среднее время разговора по каждой АТС и по городу в целом.
CREATE OR REPLACE VIEW get_avg_call_time_by_atc AS
    SELECT a.name, AVG(c.time)
    FROM calls c
    JOIN atces a ON a.id = c.atc_id
    GROUP BY a.name;
    
SELECT * FROM get_avg_call_time_by_atc;

--Определить среднее время разговора по каждой АТС и по городу в целом.
CREATE OR REPLACE VIEW get_avg_call_time_by_city AS
    SELECT city.name, AVG(c.time)
    FROM calls c
    JOIN cities city ON city.id = c.city_id
    GROUP BY city.name;
    
SELECT * FROM get_avg_call_time_by_city;

--Определить общее количество переговоров и стоимость этих переговоров в указанный месяц отдельно по городским и по международным звонкам. 
CREATE OR REPLACE VIEW get_cities_calls AS
    SELECT COUNT(c.id), SUM(c.cost)
    FROM calls c
    WHERE Position('+7' IN c.phone) = 1;
    
SELECT * FROM get_cities_calls;

--Определить общее количество переговоров и стоимость этих переговоров в указанный месяц отдельно по городским и по международным звонкам. 
CREATE OR REPLACE VIEW get_countries_calls AS
    SELECT COUNT(c.id), SUM(c.cost)
    FROM calls c
    WHERE Position('+7' IN c.phone) != 1;
    
SELECT * FROM get_countries_calls;

